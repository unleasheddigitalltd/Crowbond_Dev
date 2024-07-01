using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;

namespace Crowbond.Modules.CRM.Presentation.Suppliers;

internal sealed class GetSuppliers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers", async (
            ISender sender,
            string search = "",
            string sort = "suppliername",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<SuppliersResponse> result = await sender.Send(
                new GetSuppliersQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetSuppliers)
        .WithTags(Tags.Suppliers);
    }
}
