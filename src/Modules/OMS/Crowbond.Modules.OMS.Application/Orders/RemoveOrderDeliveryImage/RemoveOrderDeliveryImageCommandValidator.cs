using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderDeliveryImage;

internal sealed class RemoveOrderDeliveryImageCommandValidator: AbstractValidator<RemoveOrderDeliveryImageCommand>
{
    public RemoveOrderDeliveryImageCommandValidator()
    {
        RuleFor(d => d.ImageName).NotEmpty().MaximumLength(100);
    }
}
