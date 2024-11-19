using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Settings;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandHandler(
    ICustomerApi customerApi,
    ISettingRepository settingRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateOrderCommand>
{
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), request.PaymentMethod))
        {
            return Result.Failure(OrderErrors.InvalidPaymentMethod);
        }

        if (!Enum.IsDefined(typeof(DeliveryMethod), request.DeliveryMethod))
        {
            return Result.Failure(OrderErrors.InvalidDeliveryMethod);
        }

        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (orderHeader == null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        CustomerForOrderResponse? customer = await customerApi.GetAsync(orderHeader.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(orderHeader.CustomerId));
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
            return Result.Failure(SettingErrors.NotFound);
        }

        // check the outlet is belong to the customer
        if (outlet.CustomerId != customer.Id)
        {
            return Result.Failure(CustomerErrors.InvalidOutletForCustomer);
        }

        // get delivery charge
        decimal deliveryCharge = (DeliveryFeeSetting)customer.DeliveryFeeSetting switch
        {
            DeliveryFeeSetting.None => 0,
            DeliveryFeeSetting.Global => setting.DeliveryCharge,
            DeliveryFeeSetting.Custom => customer.DeliveryCharge,
            _ => throw new NotImplementedException()
        };


        Result result = orderHeader.Update(
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

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
