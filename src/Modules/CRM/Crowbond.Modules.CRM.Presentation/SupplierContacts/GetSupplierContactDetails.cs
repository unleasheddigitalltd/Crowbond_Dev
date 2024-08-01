using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContactDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierContacts;

internal sealed class GetSupplierContactDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/contact/{id}/details", async (Guid id, ISender sender) =>
        {
            Result<SupplierContactDetailsResponse> result = await sender.Send(new GetSupplierContactDetailsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomers)
            .WithTags(Tags.Suppliers);
    }
}
