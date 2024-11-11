using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Abstractions.Authentication;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetMyCustomerProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class GetMyCustomerProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/my/products", async (
            IContactContext contactContext,
            ISender sender,
            string search = "",
            string sort = "ProductSku",
            string order = "asc",
            int page = 0,
            int size = 10) =>
        {
            Result<CustomerProductsResponse> result = await sender.Send(new GetMyCustomerProductsQuery(contactContext.ContactId, search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetCustomers)
            .WithTags(Tags.Customers);
    }
}
