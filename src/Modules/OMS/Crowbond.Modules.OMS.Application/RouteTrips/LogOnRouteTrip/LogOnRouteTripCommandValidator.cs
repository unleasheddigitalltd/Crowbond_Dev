using FluentValidation;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOnRouteTrip;

internal sealed class LogOnRouteTripCommandValidator : AbstractValidator<LogOnRouteTripCommand>
{
    public LogOnRouteTripCommandValidator()
    {
        RuleFor(t => t.DriverId).NotEmpty();
        RuleFor(r => r.RouteTripId).NotEmpty();
    }
}
