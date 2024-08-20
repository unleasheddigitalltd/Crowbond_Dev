using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactActivation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerContacts;

internal sealed class UpdateCustomerContactActivation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/contacts/{id}/activation/{isActive}", async (ClaimsPrincipal claims, Guid id, bool isActive, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerContactActivationCommand(claims.GetUserId(), id, isActive));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyCustomerContacts)
        .WithTags(Tags.Customers);
    }
}
