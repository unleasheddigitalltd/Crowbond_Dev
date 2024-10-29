using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Suppliers;

internal sealed class UpdateSupplier : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("suppliers/{id}", async (Guid id, SupplierRequest request, ISender sender) =>
        {

            Result result = await sender.Send(new UpdateSupplierCommand(id, request));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifySuppliers)
        .WithTags(Tags.Suppliers);
    }
}
