using Crowbond.Common.Application.Clock;
using FluentValidation;

namespace Crowbond.Modules.OMS.Application.RouteTrips.CreateRouteTrip;

internal sealed class CreateRouteTripCommandValidator : AbstractValidator<CreateRouteTripCommand>
{
    public CreateRouteTripCommandValidator()
    {
        RuleFor(r => r.RouteTrip.Date).NotEmpty();
        RuleFor(r => r.RouteTrip.RouteId).NotEmpty();
        RuleFor(r => r.RouteTrip.Comments).MaximumLength(255);
    }
}
