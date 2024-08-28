using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Stocks.UpdateStockQuantity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Stocks;

internal sealed class UpdateStockQuantity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("stocks/{id}/adjust", async (ClaimsPrincipal claims, Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateStockQuantityCommand(claims.GetUserId(), id, request.TransactionNote, request.Reason, request.Quantity));
            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.AdjustStocks)
        .WithTags(Tags.Stocks);
    }

    public sealed record Request(string TransactionNote, Guid Reason, decimal Quantity);

}
