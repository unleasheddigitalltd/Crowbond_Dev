using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierContacts;

internal sealed class UpdateSupplierContact : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("suppliers/contacts/{id}", async (Guid id, SupplierContactRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateSupplierContactCommand(id, request));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifySupplierContacts)
            .WithTags(Tags.Suppliers);
    }
}
