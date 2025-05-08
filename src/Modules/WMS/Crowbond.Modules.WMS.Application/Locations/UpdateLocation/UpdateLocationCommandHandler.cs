using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Stocks;

namespace Crowbond.Modules.WMS.Application.Locations.UpdateLocation;

internal sealed class UpdateLocationCommandHandler(
    ILocationRepository locationRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateLocationCommand>
{
    public async Task<Result> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await locationRepository.GetAsync(request.LocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure(LocationErrors.NotFound(request.LocationId));
        }

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

            if (request.LocationLayer == LocationLayer.Area && parent.LocationLayer != LocationLayer.Site)
            {
                return Result.Failure<Guid>(LocationErrors.InvalidParentLayerForLocation);
            }

            if (request.LocationLayer == LocationLayer.Site)
            {
                return Result.Failure<Guid>(LocationErrors.CanNotHaveParent);
            }
        }

        IEnumerable<Location> children = await locationRepository.GetChildrenAsync(request.LocationId, cancellationToken);

        if (children.Any() && location.LocationLayer != request.LocationLayer)
        {
            return Result.Failure(LocationErrors.HasChild(request.LocationId));
        }

        IEnumerable<Stock> stocks = await stockRepository.GetByLocationAsync(request.LocationId, cancellationToken);

        if (stocks.Any() && request.LocationLayer != LocationLayer.Location && location.LocationType != request.LocationType)
        {
            return Result.Failure(LocationErrors.HasStock(request.LocationId));
        }

        location.Update(request.ParentId, request.Name, request.ScanCode, request.NetworkAddress, request.PrinterName, request.LocationType, request.LocationLayer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
