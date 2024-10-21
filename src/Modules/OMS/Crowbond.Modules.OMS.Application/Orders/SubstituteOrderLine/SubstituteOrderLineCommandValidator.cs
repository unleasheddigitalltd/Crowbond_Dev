using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLine;

internal sealed class SubstituteOrderLineCommandValidator : AbstractValidator<SubstituteOrderLineCommand>
{
    public SubstituteOrderLineCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
        RuleFor(ol => ol.ProductId).NotEmpty();
    }
}
