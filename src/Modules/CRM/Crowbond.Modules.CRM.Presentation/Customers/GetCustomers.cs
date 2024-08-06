using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.CRM.Application.Customers.GetCustomers;

namespace Crowbond.Modules.CRM.Presentation.Customers;

internal sealed class GetCustomers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers", async (
            ISender sender,
            string search = "",
            string sort = "AccountNumber",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<CustomersResponse> result = await sender.Send(
                new GetCustomersQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetCustomers)
        .WithTags(Tags.Customers);
    }
}
