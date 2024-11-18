using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrder;

internal sealed class RemoveOrderCommandValidator: AbstractValidator<RemoveOrderCommand>
{
    public RemoveOrderCommandValidator()
    {
        RuleFor(o => o.OrderId).NotEmpty();
    }
}
