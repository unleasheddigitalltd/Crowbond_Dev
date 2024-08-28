﻿using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Customers.UploadCustomerLogo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Customers;

internal sealed class UploadCustomerLogo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{id}/logo", async (ClaimsPrincipal claims, Guid id, IFormFile logo, ISender sender) =>
        {
            Result result = await sender.Send(new UploadCustomerLogoCommand(claims.GetUserId(), id, logo));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomers)
            .WithTags(Tags.Customers)
            .DisableAntiforgery();
    }
}