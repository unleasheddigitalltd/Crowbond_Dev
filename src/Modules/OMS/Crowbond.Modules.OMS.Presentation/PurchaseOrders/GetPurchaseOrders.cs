﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class GetPurchaseOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("purchaseorders", async (
            ISender sender,
            string search = "",
            string sort = "suppliername",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<PurchaseOrdersResponse> result = await sender.Send(
                new GetPurchaseOrdersQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permission.GetPurchaseOrders)
        .WithTags(Tags.PurchaseOrders);
    }
}