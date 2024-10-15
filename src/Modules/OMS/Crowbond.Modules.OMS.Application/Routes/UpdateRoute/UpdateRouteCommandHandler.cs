using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Routes;

namespace Crowbond.Modules.OMS.Application.Routes.UpdateRoute;

internal sealed class UpdateRouteCommandHandler(
    IRouteRepository routeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRouteCommand>
{
    public async Task<Result> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
    {
        Route? route = await routeRepository.GetAsync(request.RouteId, cancellationToken);

        if (route == null)
        {
            return Result.Failure(RouteErrors.NotFound(request.RouteId));
        }

        route.Update(request.Name, request.Position, request.CutOffTime, request.DaysOfWeek);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
