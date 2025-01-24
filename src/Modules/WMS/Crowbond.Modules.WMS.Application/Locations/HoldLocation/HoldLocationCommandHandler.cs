using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.HoldLocation;

internal sealed class HoldLocationCommandHandler(
    ILocationRepository locationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<HoldLocationCommand>
{
    public async Task<Result> Handle(HoldLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await locationRepository.GetAsync(request.LocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure(LocationErrors.NotFound(request.LocationId));
        }

        Result result = location.Hold();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
