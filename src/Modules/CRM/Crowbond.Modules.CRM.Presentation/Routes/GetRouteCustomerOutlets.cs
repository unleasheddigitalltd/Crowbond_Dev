using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Routes.GetRouteCustomerOutlets;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Routes;

internal sealed class GetRouteCustomerOutlets : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes/{id}/customer-outlets", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<CustomerOutletResponse>> result = await sender.Send(new GetRouteCustomerOutletsQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetRoutes)
            .WithTags(Tags.Routes);
    }
}
