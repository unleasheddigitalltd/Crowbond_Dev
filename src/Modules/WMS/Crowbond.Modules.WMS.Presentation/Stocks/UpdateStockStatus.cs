using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Stocks.UpdateStockStatus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Stocks;

internal sealed class UpdateStockStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("stocks/{id}/{statusType}", async(Guid id, string statusType, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateStockStatusCommand(id, statusType));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.AdjustStocks)
            .WithTags(Tags.Stocks);
    }
}
