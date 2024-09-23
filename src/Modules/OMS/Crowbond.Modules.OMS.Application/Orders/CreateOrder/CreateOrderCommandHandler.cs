using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Products;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Settings;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    ICustomerApi customerApi,
    ICustomerProductApi customerProductApi,
    ISettingRepository settingRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), request.Order.PaymentMethod))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidPaymentMethod);
        }

        if (!Enum.IsDefined(typeof(DeliveryMethod), request.Order.DeliveryMethod))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidDeliveryMethod);
        }

        CustomerForOrderResponse? customer = await customerApi.GetAsync(request.Order.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.Order.CustomerId));
        }

        if (!Enum.IsDefined(typeof(PaymentTerm), customer.PaymentTerms))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidPaymentTerm);
        }

        if (!Enum.IsDefined(typeof(DeliveryFeeSetting), customer.DeliveryFeeSetting))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidDeliveryFeeSetting);
        }

        CustomerOutletForOrderResponse? outlet = await customerApi.GetOutletForOrderAsync(request.Order.CustomerOutletId, cancellationToken);

        if (outlet is null)
        {
            return Result.Failure<Guid>(CustomerErrors.OutletNotFound(request.Order.CustomerOutletId));
        }

        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure<Guid>(SettingErrors.NotFound);
        }

        // check the outlet is belong to the customer
        if (outlet.CustomerId != customer.Id)
        {
            return Result.Failure<Guid>(CustomerErrors.InvalidOutletForCustomer);
        }

        // get delivery charge
        decimal deliveryCharge = (DeliveryFeeSetting)customer.DeliveryFeeSetting switch
        {
            DeliveryFeeSetting.None => 0,
            DeliveryFeeSetting.Global => setting.DeliveryCharge,
            DeliveryFeeSetting.Custom => customer.DeliveryCharge,
            _ => throw new NotImplementedException()
        };

        Sequence? sequence = await orderRepository.GetSequenceAsync(cancellationToken);

        if (sequence is null)
        {
            return Result.Failure<Guid>(OrderErrors.SequenceNotFound);
        }


        Result<OrderHeader> result = OrderHeader.Create(
            sequence.GetNumber(),
            null,
            customer.Id,
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
            request.Order.ShippingDate,
            (DeliveryMethod)request.Order.DeliveryMethod,
            deliveryCharge,
            (PaymentTerm)customer.PaymentTerms,
            (PaymentMethod)request.Order.PaymentMethod,
            request.Order.CustomerComment,
            dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        foreach (OrderLineRequest line in request.Order.Lines)
        {
            CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customer.Id, line.ProductId, cancellationToken);

            if (customerProduct is null)
            {
                return Result.Failure<Guid>(CustomerProductErrors.NotFound(customer.Id, line.ProductId));
            }

            if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
            {
                return Result.Failure<Guid>(CustomerProductErrors.InvalidTaxRateType);
            }

            decimal unitPrice = (customer.NoDiscountFixedPrice && customerProduct.IsFixedPrice) ?
                customerProduct.UnitPrice :
                customerProduct.UnitPrice * ((100 - customer.Discount) / 100);

            result.Value.AddLine(
                customerProduct.ProductId,
                customerProduct.ProductSku,
                customerProduct.ProductName,
                customerProduct.UnitOfMeasureName,
                unitPrice,
                line.Qty,
                (TaxRateType)customerProduct.TaxRateType);
        }

        orderRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
