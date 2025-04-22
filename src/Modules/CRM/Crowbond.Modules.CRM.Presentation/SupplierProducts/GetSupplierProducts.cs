using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;
using Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;
using Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProductsWithStock;
using Crowbond.Modules.WMS.Application.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierProducts;

internal sealed class GetSupplierProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/{id}/products", async (Guid id, ISender sender) =>
            {
                Result<IReadOnlyCollection<SupplierProductResponse>> result =
                    await sender.Send(new GetSupplierProductsQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).RequireAuthorization(Permissions.GetSuppliers)
            .WithTags(Tags.Suppliers);
        
        app.MapGet("suppliers/{id}/products/with-stock", async (Guid id, ISender sender) =>
            {
                Result<IReadOnlyCollection<ProductWithStockResponse>> result =
                    await sender.Send(new GetSupplierProductsWithStockQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).RequireAuthorization(Permissions.GetSuppliers)
            .WithTags(Tags.Suppliers);
    }
}
