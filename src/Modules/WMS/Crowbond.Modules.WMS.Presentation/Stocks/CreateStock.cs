using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Stocks.CreateStock;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Stocks;

internal sealed class CreateStock : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("stocks", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateStockCommand(
                request.ReceiptLineId,
                request.LocationId,
                request.Qty));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.AdjustStocks)
            .WithTags(Tags.Stocks);
    }

    private sealed record Request(Guid ReceiptLineId, Guid LocationId, decimal Qty);
}
