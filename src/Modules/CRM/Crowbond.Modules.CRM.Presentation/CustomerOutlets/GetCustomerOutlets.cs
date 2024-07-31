using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutlets;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerOutlets;

internal sealed class GetCustomerOutlets : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}/outlets", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<CustomerOutletResponse>> result = await sender.Send(new GetCustomerOutletsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetCustomers)
        .WithTags(Tags.Customers);
    }
}
