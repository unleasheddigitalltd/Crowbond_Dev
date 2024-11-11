using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Compliances.UploadComplianceLineImages;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Compliances;

internal sealed class UploadComplianceLineImages : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("compliances/lines/{id}/images", async (Guid id, IFormFileCollection images, ISender sender) =>
        {
            Result result = await sender.Send(new UploadComplianceLineImagesCommand(id, images));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeliverOrders)
            .WithTags(Tags.Routes)
            .DisableAntiforgery();
    }
}
