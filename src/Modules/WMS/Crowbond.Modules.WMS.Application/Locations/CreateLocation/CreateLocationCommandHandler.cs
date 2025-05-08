using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;

namespace Crowbond.Modules.WMS.Application.Locations.CreateLocation;

internal sealed class CreateLocationCommandHandler(
    ILocationRepository locationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateLocationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId is not null)
        {
            Location? parent = await locationRepository.GetAsync((Guid)request.ParentId, cancellationToken);

            if (parent is null)
            {
                return Result.Failure<Guid>(LocationErrors.ParentNotFound((Guid)request.ParentId));
            }

            if (request.LocationLayer == LocationLayer.Area && parent.LocationLayer != LocationLayer.Site)
            {
                return Result.Failure<Guid>(LocationErrors.InvalidParentLayerForArea);
            }

            if (request.LocationLayer == LocationLayer.Location && parent.LocationLayer != LocationLayer.Area)
            {
                return Result.Failure<Guid>(LocationErrors.InvalidParentLayerForLocation);
            }

            if (request.LocationLayer == LocationLayer.Site)
            {
                return Result.Failure<Guid>(LocationErrors.CanNotHaveParent);
            }
        }

        Result<Location> result = Location.Create(
            request.ParentId,
            request.Name,
            request.ScanCode,
            request.NetworkAddress,
            request.PrinterName,
            request.LocationType,
            request.LocationLayer);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        locationRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
