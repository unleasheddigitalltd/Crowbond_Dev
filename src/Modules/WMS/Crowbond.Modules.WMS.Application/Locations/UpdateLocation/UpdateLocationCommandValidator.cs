using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Locations.UpdateLocation;

internal sealed class UpdateLocationCommandValidator: AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(l => l.LocationId).NotEmpty();
        RuleFor(l => l.Name).NotEmpty().MaximumLength(100);
        RuleFor(l => l.ScanCode).MaximumLength(20);
        RuleFor(l => l.LocationLayer).IsInEnum();
    }
}
