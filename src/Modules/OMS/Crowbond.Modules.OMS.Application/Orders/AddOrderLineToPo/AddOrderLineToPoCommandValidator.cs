using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLineToPo;

internal sealed class AddOrderLineToPoCommandValidator : AbstractValidator<AddOrderLineToPoCommand>
{
    public AddOrderLineToPoCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
    }
}
