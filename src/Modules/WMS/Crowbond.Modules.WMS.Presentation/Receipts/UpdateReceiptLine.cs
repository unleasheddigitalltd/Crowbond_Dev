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
            var result = await sender.Send(new UpdateReceiptLineCommand(
                id,
                request.QuantityReceived,
                request.UseByDate, request.SellByDate, request.Batch));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyReceipts)
            .WithTags(Tags.Receipts);
    }

    private sealed record Request(decimal QuantityReceived, string? Batch, DateOnly? UseByDate, DateOnly? SellByDate);
}
