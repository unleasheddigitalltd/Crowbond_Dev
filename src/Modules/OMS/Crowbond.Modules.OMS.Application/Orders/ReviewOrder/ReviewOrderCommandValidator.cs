using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.ReviewOrder;

internal sealed class ReviewOrderCommandValidator: AbstractValidator<ReviewOrderCommand>
{
    public ReviewOrderCommandValidator()
    {
        RuleFor(o => o.OrderHeaderId).NotEmpty();
    }
}
