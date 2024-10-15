using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.AddOrderLineToPo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class AddOrderLineToPo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("orders/lines/{id}/add-to-po", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new AddOrderLineToPoCommand(id, request.SupplierId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ReviewOrderLine)
            .WithTags(Tags.Orders);
    }

    private sealed record Request(Guid SupplierId);
}
