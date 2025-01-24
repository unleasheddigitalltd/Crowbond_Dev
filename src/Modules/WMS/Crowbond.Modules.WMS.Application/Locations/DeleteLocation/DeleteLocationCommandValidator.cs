using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Locations.DeleteLocation;

internal sealed class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationCommandValidator()
    {
        RuleFor(l => l.LocationId).NotEmpty();
    }
}
