using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Compliances.RemoveComplianceLineImage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Compliances;

internal sealed class RemoveComplianceLineImage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("compliances/lines/{id}/images", async (Guid id, string imageName, ISender sender) =>
        {
            Result result = await sender.Send(new RemoveComplianceLineImageCommand(id, imageName));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeliverOrders)
            .WithTags(Tags.Orders)
            .DisableAntiforgery();
    }
}
