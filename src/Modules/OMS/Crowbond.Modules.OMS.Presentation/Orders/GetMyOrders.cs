using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Orders.GetMyOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetMyOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/my", async (
            IContactContext contactContext,
            ISender sender,
            string search = "",
            string sort = "orderNo",
            string order = "desc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<OrdersResponse> result = await sender.Send(
                new GetMyOrdersQuery(contactContext.ContactId, search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetMyOrders)
        .WithTags(Tags.Orders);
    }
}
