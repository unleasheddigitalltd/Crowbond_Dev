using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Routes.CreateRoute;

internal sealed class CreateRouteCommandValidator: AbstractValidator<CreateRouteCommand>
{
    public CreateRouteCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Position).GreaterThan(0);
        RuleFor(r => r.CutOffTime).NotEmpty().InclusiveBetween(TimeOnly.MinValue, TimeOnly.MaxValue);
        RuleFor(r => r.DaysOfWeek).NotEmpty().Matches("^[01]{7}$");
    }
}
