﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContact;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerContacts;

internal sealed class GetCustomerContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/contacts/{id}", async (Guid id, ISender sender) =>
        {
            Result<CustomerContactResponse> result = await sender.Send(new GetCustomerContactQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomers)
            .WithTags(Tags.Customers);
    }
}
