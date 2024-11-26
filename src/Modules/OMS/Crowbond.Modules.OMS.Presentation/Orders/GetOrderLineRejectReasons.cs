using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.GetOrderLineRejectReasons;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetOrderLineRejectReasons : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/lines/rejects/reasons", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<RejectReasonResponse>> result = await sender.Send(new GetOrderLineRejectReasonsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetOrderLineRejectReasons)
            .WithTags(Tags.Orders);
    }
}
