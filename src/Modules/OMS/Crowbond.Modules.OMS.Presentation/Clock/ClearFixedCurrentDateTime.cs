using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Clock.ClearFixedCurrentDateTime;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Clock;

internal sealed class ClearFixedCurrentDateTime : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("clock/clear", async (ISender sender) =>
        {
            Result result = await sender.Send(new ClearFixedCurrentDateTimeCommand());

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .AllowAnonymous()
            .WithTags(Tags.Settings);
    }
}
