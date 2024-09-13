using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierContacts.CreateSupplierContact;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierContacts;

internal sealed class CreateSupplierContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("suppliers/{id}/contacts", async (Guid id, SupplierContactRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateSupplierContactCommand(id, request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateSupplierContacts)
            .WithTags(Tags.Suppliers);
    }
}
