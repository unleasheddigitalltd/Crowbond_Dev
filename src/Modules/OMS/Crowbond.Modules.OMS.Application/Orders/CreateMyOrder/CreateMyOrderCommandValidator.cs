using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.CreateMyOrder;

internal sealed class CreateMyOrderCommandValidator : AbstractValidator<CreateMyOrderCommand>
{
    public CreateMyOrderCommandValidator()
    {
        RuleFor(c => c.CustomerContactId).NotEmpty();
        RuleFor(c => c.CustomerOutletId).NotEmpty();
        RuleFor(c => c.ShippingDate).NotEmpty();
        RuleFor(c => c.DeliveryMethod).NotNull();
        RuleFor(c => c.PaymentMethod).NotNull();
        RuleFor(c => c.CustomerComment).MaximumLength(255);
    }
}
