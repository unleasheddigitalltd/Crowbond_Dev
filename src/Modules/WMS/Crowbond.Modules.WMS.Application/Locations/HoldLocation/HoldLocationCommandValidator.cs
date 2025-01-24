using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Locations.HoldLocation;

internal sealed class HoldLocationCommandValidator: AbstractValidator<HoldLocationCommand>
{
    public HoldLocationCommandValidator()
    {
        RuleFor(l => l.LocationId).NotEmpty();
    }
}
