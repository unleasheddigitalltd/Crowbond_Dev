using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutletActivation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerOutlets;

internal sealed class UpdateCustomerOutletActivation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/outlets/{id}/activation/{isActive}", async (ClaimsPrincipal claims, Guid id, bool isActive, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerOutletActivationCommand(claims.GetUserId(), id, isActive));

            return result.Match(Results.NoContent, ApiResults.Problem);
        }
        )
        .RequireAuthorization(Permissions.ModifyCustomerContacts)
        .WithTags(Tags.Customers);
    }
}
