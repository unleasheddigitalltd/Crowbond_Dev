using System.Globalization;
using System.Text.RegularExpressions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Settings;
using Crowbond.Common.Application.Clock;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Choco;
using Crowbond.Modules.OMS.Application.Choco.Enums;
using Crowbond.Modules.OMS.Application.Choco.Requests;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Webhooks.Choco;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Products;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Application.Webhooks.Choco
{
    public interface IChocoOrderCreatedCommandHandler
    {
        Task<Result> Handle(ChocoOrderCreatedCommand command, CancellationToken cancellationToken);
    }

    public class ChocoOrderCreatedCommandHandler(
        ICustomerApi customerApi,
        ICustomerProductApi customerProductApi,
        ISettingRepository settingRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IChocoClient chocoClient,
        IDateTimeProvider dateTimeProvider,
        ILogger<ChocoOrderCreatedCommandHandler> logger)
        : ICommandHandler<ChocoOrderCreatedCommand>, IChocoOrderCreatedCommandHandler
    {

        public async Task<Result> Handle(ChocoOrderCreatedCommand request, CancellationToken cancellationToken)
        {
            var orderData = request.webhook.Payload.Order;

            // Validate order ID
            if (string.IsNullOrWhiteSpace(orderData.Id))
            {
                logger.LogError("Missing Order ID in webhook payload.");
                return Result.Failure(new Error("", "Order ID is missing in webhook payload.", ErrorType.Problem));
            }

            // Retrieve customer
            var customer = await customerApi.GetByAccountNumberAsync(orderData.Customer.CustomerNumber, cancellationToken);
            if (customer is null)
            {
                logger.LogWarning("Customer not found: {AccountNumber}", orderData.Customer.CustomerNumber);
                return Result.Failure(CustomerErrors.NotFound());
            }

            // Retrieve settings
            var setting = await settingRepository.GetAsync(cancellationToken);
            if (setting is null)
            {
                logger.LogError("Settings not found in database.");
                return Result.Failure(SettingErrors.NotFound);
            }

            // Retrieve sequence
            var sequence = await orderRepository.GetSequenceAsync(cancellationToken);
            if (sequence is null)
            {
                logger.LogError("Order sequence not found.");
                return Result.Failure(OrderErrors.SequenceNotFound);
            }

            // Determine outlet
            string postcode = !string.IsNullOrEmpty(orderData.Customer.DeliveryAddress.PostalCode)
                ? orderData.Customer.DeliveryAddress.PostalCode
                : Regex.Match(orderData.Customer.DeliveryAddress.Full, @"[A-Z]{1,2}[0-9R][0-9A-Z]? [0-9][ABD-HJLNP-UW-Z]{2}").Value;

            var outlet = await customerApi.GetOutletForOrderByPostcodeAsync(postcode, customer.Id, cancellationToken);
            if (outlet is null)
            {
                logger.LogWarning("Outlet not found for customer {CustomerId} at postcode {Postcode}.", customer.Id, postcode);
                return Result.Failure(CustomerErrors.OutletNotFound(customer.Id));
            }
            if (outlet.CustomerId != customer.Id)
            {
                logger.LogWarning("Invalid outlet for customer: OutletCustomerId={OutletCustomerId}, CustomerId={CustomerId}.", outlet.CustomerId, customer.Id);
                return Result.Failure(CustomerErrors.InvalidOutletForCustomer);
            }

            // Calculate delivery charge
            decimal deliveryCharge = (DeliveryFeeSetting)customer.DeliveryFeeSetting switch
            {
                DeliveryFeeSetting.None => 0,
                DeliveryFeeSetting.Global => setting.DeliveryCharge,
                DeliveryFeeSetting.Custom => customer.DeliveryCharge,
                _ => throw new NotImplementedException(),
            };

            // Create order header
            var orderResult = OrderHeader.Create(
                sequence.GetNumber(),
                orderData.ReferenceNumber,
                customer.Id,
                outlet.Id,
                customer.AccountNumber,
                customer.BusinessName,
                outlet.LocationName,
                outlet.FullName,
                outlet.Email,
                outlet.PhoneNumber,
                outlet.Mobile,
                outlet.DeliveryNote,
                outlet.AddressLine1,
                outlet.AddressLine2,
                outlet.TownCity,
                outlet.County,
                outlet.Country,
                outlet.PostalCode,
                DateOnly.FromDateTime(orderData.DeliveryDate),
                DeliveryMethod.delivery,
                deliveryCharge,
                DueDateCalculationBasis.EndOfInvoiceMonth,
                customer.DueDaysForInvoice,
                PaymentMethod.CashOnDelivery,
                orderData.Comment,
                dateTimeProvider.UtcNow);

            if (orderResult.IsFailure)
            {
                logger.LogError("OrderHeader creation failed: {Error}", orderResult.Error);
                return Result.Failure(orderResult.Error);
            }

            var order = orderResult.Value;

            // Add products: log and collect any errors without stopping the order
            var lineErrors = await AddOrderLinesAsync(order, orderData.Products, customer.Id, cancellationToken);
            if (lineErrors.Any())
            {
                foreach (var err in lineErrors)
                {
                    logger.LogWarning("Order {OrderId}: line addition error: {ErrorCode} - {ErrorMessage}",
                        order.Id, err.Code, err.Description);
                }
            }

            // Persist and finalize
            orderRepository.Insert(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await chocoClient.UpdateActionStatusAsync(
                new UpdateActionStatusRequest
                {
                    ActionId = request.webhook.ActionId,
                    Status = ChocoActionStatus.Succeeded,
                    Details = new { OrderId = order.Id }
                },
                cancellationToken);

            return Result.Success();
        }

        private async Task<List<Error>> AddOrderLinesAsync(
            OrderHeader order,
            IEnumerable<ChocoOrderProduct> products,
            Guid customerId,
            CancellationToken ct)
        {
            var errors = new List<Error>();

            foreach (var product in products)
            {
                try
                {
                    var custProd = await customerProductApi.GetBySkuAsync(customerId, product.Product.ExternalId, ct);
                    if (custProd is null)
                    {
                        var err = CustomerProductErrors.SkuNotFound(customerId, product.Product.ExternalId);
                        logger.LogWarning("Customer product not found: {Sku} for customer {CustomerId}.", product.Product.ExternalId, customerId);
                        errors.Add(err);
                        continue;
                    }

                    if (custProd.IsBlacklisted)
                    {
                        var err = OrderErrors.ProductIsBlacklisted(custProd.ProductId);
                        logger.LogWarning("Product blacklisted: {ProductId}.", custProd.ProductId);
                        errors.Add(err);
                        continue;
                    }

                    if (!Enum.IsDefined(typeof(TaxRateType), custProd.TaxRateType))
                    {
                        logger.LogWarning("Invalid tax rate type: {TaxRateType} for product {ProductId}.", custProd.TaxRateType, custProd.ProductId);
                        errors.Add(CustomerProductErrors.InvalidTaxRateType);
                        continue;
                    }

                    if (order.Lines.Any(l => l.ProductId == custProd.ProductId))
                    {
                        var err = OrderErrors.LineForProductExists(custProd.ProductId);
                        logger.LogWarning("Duplicate product line: {ProductId} in order {OrderId}.", custProd.ProductId, order.Id);
                        errors.Add(err);
                        continue;
                    }

                    var quantity = decimal.Parse(product.Quantity, CultureInfo.InvariantCulture);
                    var lineResult = order.AddLine(
                        custProd.ProductId,
                        custProd.ProductSku,
                        custProd.ProductName,
                        custProd.UnitOfMeasureName,
                        custProd.CategoryId,
                        custProd.CategoryName,
                        custProd.BrandId,
                        custProd.BrandName,
                        custProd.ProductGroupId,
                        custProd.ProductGroupName,
                        custProd.UnitPrice,
                        quantity,
                        false,
                        (TaxRateType)custProd.TaxRateType);

                    if (lineResult.IsFailure)
                    {
                        logger.LogWarning("Order {OrderId}: failed to add line for product {ProductId}: {Error}",
                            order.Id, custProd.ProductId, lineResult.Error.Description);
                        errors.Add(lineResult.Error);
                        continue;
                    }

                    orderRepository.AddLine(lineResult.Value);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Exception adding product {Sku} to order {OrderId}.", product.Product.ExternalId, order.Id);
                    errors.Add(new Error("Exception", ex.Message, ErrorType.Problem));
                }
            }

            return errors;
        }
    }
}
