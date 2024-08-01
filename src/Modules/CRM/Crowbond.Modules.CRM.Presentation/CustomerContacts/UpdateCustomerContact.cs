﻿using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerContacts;

internal sealed class UpdateCustomerContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("customers/contacts/{id}", async (Guid id, ClaimsPrincipal claims, CustomerContactRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerContactCommand(claims.GetUserId(), id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomerContacts)
            .WithTags(Tags.Customers);
    }
}
