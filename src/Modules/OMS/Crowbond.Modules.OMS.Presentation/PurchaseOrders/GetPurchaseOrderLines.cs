using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class GetPurchaseOrderLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("purchase-order/{id}/lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<PurchaseOrderLineResponse>> result = await sender.Send(new GetPurchaseOrderLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
