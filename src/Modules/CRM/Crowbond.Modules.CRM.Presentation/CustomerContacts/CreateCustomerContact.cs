using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerContacts;

internal sealed class CreateCustomerContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{id}/contacts", async (Guid id, CustomerContactRequest request, ClaimsPrincipal claimes, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateCustomerContactCommand(id, claimes.GetUserId(), request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateCustomerContacts)
            .WithTags(Tags.Customers);
    }
}
