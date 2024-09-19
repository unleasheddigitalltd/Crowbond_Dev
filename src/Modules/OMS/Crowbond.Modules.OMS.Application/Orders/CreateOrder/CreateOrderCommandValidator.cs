using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.CustomerContactId).NotEmpty();
        RuleFor(c => c.CustomerOutletId).NotEmpty();
        RuleFor(c => c.ShippingDate).NotEmpty();
        RuleFor(c => c.DeliveryMethod).NotNull();
        RuleFor(c => c.PaymentMethod).NotNull();
        RuleFor(c => c.CustomerComment).MaximumLength(255);
    }
}
