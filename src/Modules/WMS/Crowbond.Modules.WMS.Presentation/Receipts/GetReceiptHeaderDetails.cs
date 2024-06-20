using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaderDetails.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class GetReceiptHeaderDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("receipts/{id}", async (Guid id, ISender sender) =>
        {
            Result<ReceiptHeaderDetailsResponse> result = await sender.Send(new GetReceiptHeaderDetailsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetReceipts)
            .WithTags(Tags.Receipts);
    }
}
