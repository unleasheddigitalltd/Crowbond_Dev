using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Settings;
using Crowbond.Modules.OMS.PublicApi;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    ICustomerApi customerApi,
    IRouteTripApi routeTripApi,
    ISettingRepository settingRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), request.PaymentMethod))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidPaymentMethod);
        }

        if (!Enum.IsDefined(typeof(DeliveryMethod), request.DeliveryMethod))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidDeliveryMethod);
        }

        CustomerForOrderResponse? customer = await customerApi.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.CustomerId));
        }

        if (!Enum.IsDefined(typeof(DueDateCalculationBasis), customer.DueDateCalculationBasis))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidDueDateCalculationBasis);
        }

        if (!Enum.IsDefined(typeof(DeliveryFeeSetting), customer.DeliveryFeeSetting))
        {
            return Result.Failure<Guid>(OrderErrors.InvalidDeliveryFeeSetting);
        }

        CustomerOutletForOrderResponse? outlet = await customerApi.GetOutletForOrderAsync(request.CustomerOutletId, cancellationToken);

        if (outlet is null)
        {
            return Result.Failure<Guid>(CustomerErrors.OutletNotFound(request.CustomerOutletId));
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


        var result = OrderHeader.Create(
            sequence.GetNumber(),
            null,
            customer.Id,
            request.CustomerOutletId,
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
            return Result.Failure<Guid>(result.Error);
        }
        
        var order = result.Value;
        orderRepository.Insert(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await routeTripApi.AssignRouteTripToOrderAsync(order.Id, cancellationToken);
        return Result.Success(order.Id);
    }
}
