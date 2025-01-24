using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Locations.ActiveLocation;

internal sealed class ActiveLocationCommandValidator: AbstractValidator<ActiveLocationCommand>
{
    public ActiveLocationCommandValidator()
    {
        RuleFor(l => l.LocationId).NotEmpty();
    }
}
