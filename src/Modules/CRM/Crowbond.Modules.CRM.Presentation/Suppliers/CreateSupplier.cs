using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Suppliers;

internal sealed class CreateSupplier : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Suppliers", async (SupplierRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateSupplierCommand(request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateSuppliers)
        .WithTags(Tags.Suppliers);
    }
}
