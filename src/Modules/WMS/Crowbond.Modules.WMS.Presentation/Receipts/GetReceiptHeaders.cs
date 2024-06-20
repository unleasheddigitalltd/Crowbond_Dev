using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts;
using Crowbond.Modules.WMS.Application.Receipts.GetReceipts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class GetReceiptHeaders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("receipts", async (
            ISender sender,
            string search = "",
            string sort = "receivedDate",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<ReceiptHeadersResponse> result = await sender.Send(
                new GetReceiptHeadersQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetReceipts)
        .WithTags(Tags.Receipts);
    }
}
