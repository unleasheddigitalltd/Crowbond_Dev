using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.GetRouteOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetOrdersByRouteId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/routes/{id}", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<OrderResponse>> reault = await sender.Send(new GetRouteOrdersQuery(id));

            return reault.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetOrders)
            .WithTags(Tags.Orders);
    }
}
