using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.UpdateReceiptLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class UpdateReceiptLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("receipts/lines/{id}", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateReceiptLineCommand(
                id,
                request.QuantityReceived,
                request.UnitPrice));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyReceipts)
            .WithTags(Tags.Receipts);
    }

    private sealed record Request(decimal QuantityReceived, decimal UnitPrice);
}
