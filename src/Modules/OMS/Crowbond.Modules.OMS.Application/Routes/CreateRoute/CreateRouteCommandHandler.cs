using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Routes;

namespace Crowbond.Modules.OMS.Application.Routes.CreateRoute;

internal sealed class CreateRouteCommandHandler(
    IRouteRepository routeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRouteCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
    {
        var route = Route.Create(
            request.Name,
            request.Position,
            request.CutOffTime,
            request.DaysOfWeek);

        routeRepository.Insert(route);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(route.Id);
    }
}
