using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Receipts;

internal sealed class CreateReceipt : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("receipts", async (ReceiptRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateReceiptCommand(request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateReceipts)
        .WithTags(Tags.Receipts);
    }
}
