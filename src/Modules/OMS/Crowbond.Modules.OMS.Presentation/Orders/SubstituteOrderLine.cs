using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class SubstituteOrderLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("orders/lines/{id}/substitute", async(Guid id, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new SubstituteOrderLineCommand(id, request.ProductId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.AcceptOrders)
            .WithTags(Tags.Orders);
    }

    private sealed record Request(Guid ProductId);
}
