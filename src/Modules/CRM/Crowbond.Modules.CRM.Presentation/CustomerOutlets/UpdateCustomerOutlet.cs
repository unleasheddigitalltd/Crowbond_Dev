﻿using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerOutlets;

internal sealed class UpdateCustomerOutlet : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("customers/outlets/{id}", async (Guid id, ClaimsPrincipal claims, CustomerOutletRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerOutletCommand(claims.GetUserId(), id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomerOutlets)
            .WithTags(Tags.Customers);
    }
}