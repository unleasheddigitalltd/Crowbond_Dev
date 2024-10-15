using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrder;

internal sealed class DeliverOrderCommandValidator: AbstractValidator<DeliverOrderCommand>
{
    public DeliverOrderCommandValidator()
    {
        RuleFor(d => d.OrderHeaderId).NotEmpty();
        RuleFor(d => d.DriverId).NotEmpty();
    }
}
