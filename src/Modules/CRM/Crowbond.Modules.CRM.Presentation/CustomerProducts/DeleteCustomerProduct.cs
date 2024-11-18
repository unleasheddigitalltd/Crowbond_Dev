using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class DeleteCustomerProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("customers/{customerId}/products/{productId}", async (Guid customerId, Guid productId, ISender sender) =>
        {
            Result result = await sender.Send(new DeleteCustomerProductCommand(customerId, productId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeleteCustomerProducts)
            .WithTags(Tags.Customers);
    }
}
