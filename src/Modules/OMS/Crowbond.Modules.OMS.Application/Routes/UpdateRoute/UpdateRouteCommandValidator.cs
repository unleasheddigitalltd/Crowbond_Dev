using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Routes.UpdateRoute;

internal sealed class UpdateRouteCommandValidator: AbstractValidator<UpdateRouteCommand>
{
    public UpdateRouteCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Position).GreaterThan(0);
        RuleFor(r => r.CutOffTime).NotEmpty().InclusiveBetween(TimeOnly.MinValue, TimeOnly.MaxValue);
        RuleFor(r => r.DaysOfWeek).NotEmpty().Length(7);
    }
}
