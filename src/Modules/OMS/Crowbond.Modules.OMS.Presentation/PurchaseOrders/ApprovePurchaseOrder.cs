using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.ApprovePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class ApprovePurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchase-orders/{id}/approve", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new ApprovePurchaseOrderCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        }
        )
            .RequireAuthorization(Permissions.ApprovePurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
