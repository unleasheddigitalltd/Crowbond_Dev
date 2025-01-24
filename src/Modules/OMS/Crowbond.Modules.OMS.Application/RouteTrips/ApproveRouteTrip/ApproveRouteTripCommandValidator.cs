using FluentValidation;

namespace Crowbond.Modules.OMS.Application.RouteTrips.ApproveRouteTrip;

internal sealed class ApproveRouteTripCommandValidator: AbstractValidator<ApproveRouteTripCommand>
{
    public ApproveRouteTripCommandValidator()
    {
        RuleFor(r => r.RouteTripId).NotEmpty();
    }
}
