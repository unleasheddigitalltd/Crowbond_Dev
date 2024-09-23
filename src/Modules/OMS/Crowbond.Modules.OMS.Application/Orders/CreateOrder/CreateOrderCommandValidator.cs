using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(r => r.Order.CustomerId).NotEmpty();
        RuleFor(r => r.Order.CustomerOutletId).NotEmpty();
        RuleFor(r => r.Order.ShippingDate).NotEmpty();
        RuleFor(r => r.Order.DeliveryMethod).NotNull();
        RuleFor(r => r.Order.PaymentMethod).NotNull();
        RuleFor(r => r.Order.CustomerComment).MaximumLength(255);

        RuleForEach(o => o.Order.Lines)
            .ChildRules(l =>
            {
                l.RuleFor(l => l.ProductId).NotEmpty();
                l.RuleFor(l => l.Qty).GreaterThan(decimal.Zero);
            });
    }
}
