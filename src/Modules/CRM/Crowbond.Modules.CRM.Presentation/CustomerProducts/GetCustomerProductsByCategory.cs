using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetCustomerProductsByCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{customerId}/categories/{categoryId}/products", async (
            Guid customerId,
            Guid categoryId,
            ISender sender,
            string search = "",
            string sort = "ProductSku",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<CustomerProductsResponse> result = await sender.Send(
                new GetCustomerProductsByCategoryQuery(
                    customerId,
                    categoryId,
                    search,
                    sort,
                    order,
                    page,
                    size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomerProducts)
            .WithTags(Tags.Customers);
    }
}
