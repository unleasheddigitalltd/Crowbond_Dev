using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerOutlets.CreateCustomerOutlet;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerOutlets;

internal sealed class CreateCustomerOutlet : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{id}/outlets", async (Guid id, ClaimsPrincipal claims, CustomerOutletRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateCustomerOutletCommand(id, claims.GetUserId(), request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateCustomerOutlets)
            .WithTags(Tags.Customers);
    }
}
