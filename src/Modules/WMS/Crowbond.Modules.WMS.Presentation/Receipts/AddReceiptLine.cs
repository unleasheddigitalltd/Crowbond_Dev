using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.AddReceiptLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class AddReceiptLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("receipts/{id}/lines", async (Guid id, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new AddReceiptLineCommand(
                id, 
                request.ProductId, 
                request.ReceivedQty, 
                request.UnitPrice));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateReceipts)
            .WithTags(Tags.Receipts);
    }

    private sealed record Request(Guid ProductId, decimal ReceivedQty, decimal UnitPrice);
}
