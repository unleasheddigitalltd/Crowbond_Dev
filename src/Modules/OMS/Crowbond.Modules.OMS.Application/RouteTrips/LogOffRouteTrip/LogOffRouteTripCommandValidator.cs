using FluentValidation;

namespace Crowbond.Modules.OMS.Application.RouteTrips.LogOffRouteTrip;
internal sealed class LogOffRouteTripCommandValidator : AbstractValidator<LogOffRouteTripCommand>
{
    public LogOffRouteTripCommandValidator()
    {
        RuleFor(t => t.DriverId).NotEmpty();
        RuleFor(r => r.RouteTripId).NotEmpty();
    }
}
