using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.ActiveLocation;

internal sealed class ActiveLocationCommandHandler(
    ILocationRepository locationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ActiveLocationCommand>
{
    public async Task<Result> Handle(ActiveLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await locationRepository.GetAsync(request.LocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure(LocationErrors.NotFound(request.LocationId));
        }

        Result result = location.Activate();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
