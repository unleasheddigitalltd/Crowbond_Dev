using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrderLines.GetPurchaseOrderLine;
using Crowbond.Modules.OMS.Application.PurchaseOrderLines.GetPurchaseOrderLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrderLines;

internal sealed class GetPurchaseOrderLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("purchase-orders/lines/{id}", async (Guid id, ISender sender) =>
        {
            Result<PurchaseOrderLineResponse> results = await sender.Send(new GetPurchaseOrderLineQuery(id));

            return results.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
