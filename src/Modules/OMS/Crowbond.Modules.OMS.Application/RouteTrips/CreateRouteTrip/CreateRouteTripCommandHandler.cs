using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.RouteTrips.CreateRouteTrip;

internal sealed class CreateRouteTripCommandHandler
    (IRouteTripRepository routeTripRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRouteTripCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRouteTripCommand request, CancellationToken cancellationToken)
    {
        if (request.RouteTrip.Date < DateOnly.FromDateTime(dateTimeProvider.UtcNow))
        {
            return Result.Failure<Guid>(RouteTripErrors.DateInPast);
        }

        Result<RouteTrip> result = RouteTrip.Create(request.RouteTrip.Date, request.RouteTrip.RouteId, request.RouteTrip.Comments);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        routeTripRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
