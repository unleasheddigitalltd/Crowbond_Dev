using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBlacklist;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetCustomerProductBlacklist : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{customerId}/product-blacklist/{productId}", async (Guid customerId, Guid productId, ISender sender) =>
        {
            Result<ProductResponse> result = await sender.Send(new GetCustomerProductBlacklistQuery(customerId, productId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomerProductBlacklist)
            .WithTags(Tags.Customers);
    }
}
