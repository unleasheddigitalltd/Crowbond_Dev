using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Reports.GetStockBuyersReport;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Reports;

internal sealed class GetStockBuyersReport : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/stock-buyers", async (
            ISender sender) =>
        {
            Result<StockBuyersReportResponse> result = await sender.Send(
                new GetStockBuyersReportQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetOrders)
        .WithTags(Tags.Orders)
        .WithName("GetStockBuyersReport")
        .WithDisplayName("Get Stock Buyers Report");
    }
}
