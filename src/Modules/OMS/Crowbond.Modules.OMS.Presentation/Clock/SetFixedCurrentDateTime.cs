using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Clock.SetFixedCurrentDateTime;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Clock;

internal sealed class SetFixedCurrentDateTime : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("clock/set", async (Request request, ISender sender) =>
        {
            Result result = await sender.Send(new SetFixedCurrentDateTimeCommand(request.CurrentDate));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .AllowAnonymous()
            .WithTags(Tags.Settings);
    }

    private sealed record Request(DateTime CurrentDate);
}
