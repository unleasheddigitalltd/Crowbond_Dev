using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(r => r.CustomerId).NotEmpty();
        RuleFor(r => r.CustomerOutletId).NotEmpty();
        RuleFor(r => r.ShippingDate).NotEmpty();
        RuleFor(r => r.DeliveryMethod).NotNull();
        RuleFor(r => r.PaymentMethod).NotNull();
        RuleFor(r => r.CustomerComment).MaximumLength(255);
    }
}
