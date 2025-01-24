﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetAllCustomerProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetAllCustomersProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/products", async (
            ISender sender,
            string search = "",
            string sort = "BusinessName",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<CustomerProductsResponse> result = await sender.Send(
                new GetAllCustomerProductsQuery(
                    search,
                    sort,
                    order,
                    page,
                    size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomerProducts)
            .WithTags(Tags.Customers);
    }
}
