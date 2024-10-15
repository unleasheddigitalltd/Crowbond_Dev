using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLineShortage;

internal sealed class SubstituteOrderLineShortageCommandValidator: AbstractValidator<SubstituteOrderLineShortageCommand>
{
    public SubstituteOrderLineShortageCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
        RuleFor(ol => ol.ProductId).NotEmpty();
    }
}
