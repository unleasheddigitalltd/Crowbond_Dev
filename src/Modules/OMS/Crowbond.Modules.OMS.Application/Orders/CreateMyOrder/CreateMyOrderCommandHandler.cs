using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Application.Carts;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Settings;

namespace Crowbond.Modules.OMS.Application.Orders.CreateMyOrder;

internal sealed class CreateMyOrderCommandHandler(
    ICustomerApi customerApi,
    ISettingRepository settingRepository,
    CartService cartService,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMyOrderCommand>
{
    public async Task<Result> Handle(CreateMyOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), request.PaymentMethod))
        {
            return Result.Failure(OrderErrors.InvalidPaymentMethod);
        }

        if (!Enum.IsDefined(typeof(DeliveryMethod), request.DeliveryMethod))
        {
            return Result.Failure(OrderErrors.InvalidDeliveryMethod);
        }

        CustomerForOrderResponse? customer = await customerApi.GetByContactIdAsync(request.CustomerContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.ContactNotFound(request.CustomerContactId));
        }

        if (!Enum.IsDefined(typeof(DueDateCalculationBasis), customer.DueDateCalculationBasis))
        {
            return Result.Failure(OrderErrors.InvalidDueDateCalculationBasis);
        }

        if (!Enum.IsDefined(typeof(DeliveryFeeSetting), customer.DeliveryFeeSetting))
        {
            return Result.Failure(OrderErrors.InvalidDeliveryFeeSetting);
        }

        CustomerOutletForOrderResponse? outlet = await customerApi.GetOutletForOrderAsync(request.CustomerOutletId, cancellationToken);

        if (outlet is null)
        {
            return Result.Failure(CustomerErrors.OutletNotFound(request.CustomerOutletId));
        }

        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure<CustomerForOrderResponse>(SettingErrors.NotFound);
        }

        // check the outlet is belong to the customer
        if (outlet.CustomerId != customer.Id)
        {
            return Result.Failure<CustomerForOrderResponse>(CustomerErrors.InvalidOutletForCustomer);
        }

        // get delivery charge
        decimal deliveryCharge = (DeliveryFeeSetting)customer.DeliveryFeeSetting switch
        {
            DeliveryFeeSetting.None => 0,
            DeliveryFeeSetting.Global => setting.DeliveryCharge,
            DeliveryFeeSetting.Custom => customer.DeliveryCharge,
            _ => throw new NotImplementedException()
        };

        Cart? cart = await cartService.GetAsync(customer.Id, cancellationToken);

        if (cart is null || cart.Items.Count == 0)
        {
            return Result.Failure(CartErrors.Empty);
        }

        Sequence? sequence = await orderRepository.GetSequenceAsync(cancellationToken);

        if (sequence is null)
        {
            return Result.Failure(OrderErrors.SequenceNotFound);
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
            request.ShippingDate,
            (DeliveryMethod)request.DeliveryMethod,
            deliveryCharge,
            (DueDateCalculationBasis)customer.DueDateCalculationBasis,
            customer.DueDaysForInvoice,
            (PaymentMethod)request.PaymentMethod,
            request.CustomerComment,
            dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        foreach (CartItem item in cart.Items)
        {
            Result<OrderLine> lineResult = result.Value.AddLine(
                item.ProductId,
                item.ProductSku,
                item.ProductName,
                item.UnitOfMeasureName,
                item.CategoryId,
                item.CategoryName,
                item.BrandId,
                item.BrandName,
                item.ProductGroupId,
                item.ProductGroupName,
                item.UnitPrice,
                item.Qty,
                item.IsBulk,
                item.TaxRateType);

            if (lineResult.IsFailure)
            {
                return Result.Failure(lineResult.Error);
            }
        }

        orderRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
