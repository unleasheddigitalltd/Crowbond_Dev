using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactActivation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierContacts;

internal sealed class UpdateSupplierContactActivation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/suppliers/contacts/{id}/activation/{isActive}", async (Guid id, bool isActive, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateSupplierContactActivationCommand(id, isActive));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifySupplierContacts)
        .WithTags(Tags.Suppliers);
    }
}
