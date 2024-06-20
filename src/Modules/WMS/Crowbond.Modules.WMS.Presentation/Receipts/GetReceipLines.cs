using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class GetReceipLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("receipts/{id}/lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<ReceiptLineResponse>> result = await sender.Send(new GetReceiptLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetReceipts)
            .WithTags(Tags.Receipts);
    }
}
