using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Stocks;

namespace Crowbond.Modules.WMS.Application.Locations.DeleteLocation;

internal sealed class DeleteLocationCommandHandler(
    ILocationRepository locationRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteLocationCommand>
{
    public async Task<Result> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await locationRepository.GetAsync(request.LocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure(LocationErrors.NotFound(request.LocationId));
        }

        IEnumerable<Location> children = await locationRepository.GetChildrenAsync(request.LocationId, cancellationToken);

        if (children.Any())
        {
            return Result.Failure(LocationErrors.HasChild(request.LocationId));
        }

        IEnumerable<Stock> stocks = await stockRepository.GetByLocationAsync(request.LocationId, cancellationToken);

        if (stocks.Any())
        {
            return Result.Failure(LocationErrors.HasStock(request.LocationId));
        }

        locationRepository.Remove(location);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
