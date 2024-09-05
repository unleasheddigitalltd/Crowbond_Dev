using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierProducts;

internal sealed class GetSupplierProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/{supplierId}/products", async (Guid supplierId, ISender sender) =>
        {
            Result<IReadOnlyCollection<SupplierProductResponse>> result = await sender.Send(new GetSupplierProductsQuery(supplierId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetSuppliers)
            .WithTags(Tags.Suppliers);
    }
}
