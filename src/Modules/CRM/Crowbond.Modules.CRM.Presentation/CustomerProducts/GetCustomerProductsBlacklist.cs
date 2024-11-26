using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductsBlacklist;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetCustomerProductsBlacklist : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{customerId}/product-blacklist", async (
            Guid customerId,
            ISender sender,
            string search = "",
            string sort = "ProductSku",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<CustomerProductsResponse> result = await sender.Send(
                new GetCustomerProductsBlacklistQuery(
                    customerId,
                    search,
                    sort,
                    order,
                    page,
                    size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomerProductBlacklist)
            .WithTags(Tags.Customers);
    }
}
