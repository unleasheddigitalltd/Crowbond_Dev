﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Customers;

internal sealed class GetCustomerDetail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}/details", async (Guid id, ISender sender) =>
        {
            Result<CustomerDetailsResponse> result = await sender.Send(new GetCustomerDetailsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetCustomers)
        .WithTags(Tags.Customers);
    }
}