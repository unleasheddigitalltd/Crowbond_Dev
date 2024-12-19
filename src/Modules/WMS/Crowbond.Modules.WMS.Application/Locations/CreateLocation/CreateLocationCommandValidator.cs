using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Locations.CreateLocation;

internal sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(l => l.Name).NotEmpty().MaximumLength(100);
        RuleFor(l => l.ScanCode).MaximumLength(20);
        RuleFor(l => l.LocationLayer).IsInEnum();
    }
}
