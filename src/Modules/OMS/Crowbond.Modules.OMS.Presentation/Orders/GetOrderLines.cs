using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.GetOrderLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetOrderLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{id}/lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<OrderLineResponse>> result = await sender.Send(new GetOrderLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetOrders)
            .WithTags(Tags.Orders);
    }
}
