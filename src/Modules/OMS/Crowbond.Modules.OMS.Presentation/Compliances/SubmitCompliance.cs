using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Compliances.SubmitCompliance;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Compliances;

internal sealed class SubmitCompliance : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("routes/trips/compliances/submit", async (IDriverContext context, Request request, ISender sender) =>
        {
            Result<bool> result = await sender.Send(new SubmitComplianceCommand(context.DriverId, request.Temprature));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeliverOrders)
            .WithTags(Tags.Routes);
    }

    private sealed record Request(decimal Temprature);
}
