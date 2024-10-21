using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Routes;

namespace Crowbond.Modules.CRM.Application.Routes.CreateRoute;

internal sealed class CreateRouteCommandHandler(
    IRouteRepository routeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRouteCommand>
{
    public async Task<Result> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
    {
        var route = Route.Create(
            request.RouteId,
            request.Name,
            request.Position,
            request.CutOffTime,
            request.DaysOfWeek);

        routeRepository.Insert(route);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
