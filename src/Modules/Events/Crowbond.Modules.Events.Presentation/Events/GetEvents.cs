using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Events.Application.Events.GetEvents;
using Crowbond.Modules.Events.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Events.Presentation.Events;

internal sealed class GetEvents : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("events", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<EventResponse>> result = await sender.Send(new GetEventsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetEvents)
        .WithTags(Tags.Events);
    }
}
