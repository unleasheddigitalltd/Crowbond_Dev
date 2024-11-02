using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Compliances.CreateCompliance;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Compliances;

internal sealed class CreateCompliance : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("routes/trips/compliances", async (
            IDriverContext context,
            Request request,
            ISender sender) =>
        {
            Result result = await sender.Send(new CreateComplianceCommand(context.DriverId, request.VehicleId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.DeliverOrders)
        .WithTags(Tags.Routes);
    }

    private sealed record Request(Guid VehicleId);
}
