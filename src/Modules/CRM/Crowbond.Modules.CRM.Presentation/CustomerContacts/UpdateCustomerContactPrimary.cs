using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactPrimary;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerContacts;

internal sealed class UpdateCustomerContactPrimary : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/contacts/{id}/primary", async (ClaimsPrincipal claims, Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerContactPrimaryCommand(claims.GetUserId(), id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyCustomerContacts)
        .WithTags(Tags.Customers);
    }
}
