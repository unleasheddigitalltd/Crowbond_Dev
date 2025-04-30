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

namespace Crowbond.Modules.OMS.Application.Webhooks.Choco;

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
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<ChocoOrderCreatedCommand>, IChocoOrderCreatedCommandHandler
{
    public async Task<Result> Handle(ChocoOrderCreatedCommand request, CancellationToken cancellationToken)
    {
        ChocoOrder orderData = request.webhook.Payload.Order;

        if (string.IsNullOrWhiteSpace(orderData.Id))
        {
            return Result.Failure(new Error("", "Order ID is missing in webhook payload.", ErrorType.Problem));
        }

        // Retrieve customer details
        CustomerForOrderResponse? customer = await customerApi.GetByAccountNumberAsync(orderData.Customer.CustomerNumber, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound());
        }

        // Retrieve settings
        Setting? setting = await settingRepository.GetAsync(cancellationToken);
        if (setting is null)
        {
            return Result.Failure(SettingErrors.NotFound);
        }

        // Retrieve sequence for order numbering
        Sequence? sequence = await orderRepository.GetSequenceAsync(cancellationToken);
        if (sequence is null)
        {
            return Result.Failure(OrderErrors.SequenceNotFound);
        }

        string postcode = orderData.Customer.DeliveryAddress.PostalCode;

        if (string.IsNullOrEmpty(postcode))
        {
            string fullDeliveryAddress = orderData.Customer.DeliveryAddress.Full;
            postcode = Regex.Match(fullDeliveryAddress, @"[A-Z]{1,2}[0-9R][0-9A-Z]? [0-9][ABD-HJLNP-UW-Z]{2}").Value;
        }
        
        CustomerOutletForOrderResponse? outlet = await customerApi.GetOutletForOrderByPostcodeAsync(postcode, customer.Id, cancellationToken);
        if (outlet is null)
        {
            return Result.Failure(CustomerErrors.OutletNotFound(customer.Id));
        }

        // Ensure outlet belongs to the correct customer
        if (outlet.CustomerId != customer.Id)
        {
            return Result.Failure(CustomerErrors.InvalidOutletForCustomer);
        }

        // Determine delivery charge
        decimal deliveryCharge = (DeliveryFeeSetting)customer.DeliveryFeeSetting switch
        {
            DeliveryFeeSetting.None => 0,
            DeliveryFeeSetting.Global => setting.DeliveryCharge,
            DeliveryFeeSetting.Custom => customer.DeliveryCharge,
            _ => throw new NotImplementedException()
        };

        // Create the new order
        Result<OrderHeader> orderResult = OrderHeader.Create(
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
            DeliveryMethod.delivery,  // Assuming delivery
            deliveryCharge,
            DueDateCalculationBasis.EndOfInvoiceMonth, // Assume
            customer.DueDaysForInvoice,
            PaymentMethod.CashOnDelivery,  // Assume
            orderData.Comment,
            dateTimeProvider.UtcNow);

        if (orderResult.IsFailure)
        {
            return Result.Failure(orderResult.Error);
        }

        OrderHeader order = orderResult.Value;

        // Add products (line items) to the order
        foreach (ChocoOrderProduct product in orderData.Products)
        {
            CustomerProductResponse? customerProduct = await customerProductApi.GetBySkuAsync(customer.Id, product.Product.ExternalId, cancellationToken);
            if (customerProduct is null)
            {
                return Result.Failure(CustomerProductErrors.SkuNotFound(customer.Id, product.Product.ExternalId));
            }

            if (customerProduct.IsBlacklisted)
            {
                return Result.Failure(OrderErrors.ProductIsBlacklisted(customerProduct.ProductId));
            }

            if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
            {
                return Result.Failure(CustomerProductErrors.InvalidTaxRateType);
            }

            // Ensure the product does not already exist in the order
            OrderLine? existingLine = order.Lines.SingleOrDefault(l => l.ProductId == customerProduct.ProductId);
            if (existingLine != null)
            {
                return Result.Failure(OrderErrors.LineForProductExists(customerProduct.ProductId));
            }

            // Add the order line
            Result<OrderLine> lineResult = order.AddLine(
                customerProduct.ProductId,
                customerProduct.ProductSku,
                customerProduct.ProductName,
                customerProduct.UnitOfMeasureName,
                customerProduct.CategoryId,
                customerProduct.CategoryName,
                customerProduct.BrandId,
                customerProduct.BrandName,
                customerProduct.ProductGroupId,
                customerProduct.ProductGroupName,
                customerProduct.UnitPrice,
                decimal.Parse(product.Quantity, CultureInfo.InvariantCulture),
                false,  // Assuming standard order, not bulk
                (TaxRateType)customerProduct.TaxRateType);

            if (lineResult.IsFailure)
            {
                return Result.Failure(lineResult.Error);
            }

            // Save the line to the order repository
            orderRepository.AddLine(lineResult.Value);
        }
        
        // Store the order
        orderRepository.Insert(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await chocoClient.UpdateActionStatusAsync(new UpdateActionStatusRequest()
        {
            ActionId = request.webhook.ActionId,
            Status = ChocoActionStatus.Succeeded,
            Details = new { OrderId = order.Id }
        }, cancellationToken);

        return Result.Success();
    }
}
