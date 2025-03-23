using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.RetryRouteAssignment;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class RetryRouteTripAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("order/{id}/route-trip-assignment", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new RetryRouteAssignmentCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        }).RequireAuthorization().WithTags(Tags.Orders);
    }
}
