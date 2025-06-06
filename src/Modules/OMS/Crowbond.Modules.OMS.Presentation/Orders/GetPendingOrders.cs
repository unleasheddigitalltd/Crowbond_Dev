﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.GetOrder;
using Crowbond.Modules.OMS.Application.Orders.GetOrders;
using Crowbond.Modules.OMS.Application.Orders.GetPendingOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetPendingOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/pending", async (
            ISender sender,
            string search = "",
            string sort = "orderNo",
            string order = "desc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<OrdersResponse> result = await sender.Send(
                new GetPendingOrdersQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetOrders)
        .WithTags(Tags.Orders);
    }
}
