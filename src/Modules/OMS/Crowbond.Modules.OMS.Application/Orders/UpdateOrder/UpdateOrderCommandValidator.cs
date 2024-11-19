using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(r => r.OrderId).NotEmpty();
        RuleFor(r => r.CustomerOutletId).NotEmpty();
        RuleFor(r => r.ShippingDate).NotEmpty();
        RuleFor(r => r.DeliveryMethod).NotNull();
        RuleFor(r => r.PaymentMethod).NotNull();
        RuleFor(r => r.CustomerComment).MaximumLength(255);
    }
}
