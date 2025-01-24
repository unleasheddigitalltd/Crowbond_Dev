using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class GetReceiptLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("receipts/lines/{id}", async (Guid id, ISender sender) =>
        {
            Result<ReceiptLineResponse> result = await sender.Send(new GetReceiptLineQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetReceipts)
            .WithTags(Tags.Receipts);
    }
}
