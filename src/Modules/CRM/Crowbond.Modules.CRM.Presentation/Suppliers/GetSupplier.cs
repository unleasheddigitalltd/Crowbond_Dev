using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Suppliers.GetSupplier;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Suppliers;

internal sealed class GetSupplier : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/{id}", async (Guid id, ISender sender) =>
        {
            Result<SupplierResponse> result = await sender.Send(new GetSupplierQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetSuppliers)
        .WithTags(Tags.Suppliers);
    }
}
