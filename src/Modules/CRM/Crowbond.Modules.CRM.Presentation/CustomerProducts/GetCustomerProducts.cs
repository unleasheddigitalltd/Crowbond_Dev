﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetCustomerProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}/products", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<CustomerProductResponse>> result = await sender.Send(new GetCustomerProductsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomers)
            .WithTags(Tags.Customers);
    }
}