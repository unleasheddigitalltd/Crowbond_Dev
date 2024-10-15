using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

internal sealed class AcceptOrderCommandValidator: AbstractValidator<AcceptOrderCommand>
{
    public AcceptOrderCommandValidator()
    {
        RuleFor(o => o.OrderId).NotEmpty();
    }
}
