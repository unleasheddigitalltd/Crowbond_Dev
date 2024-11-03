using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Orders.UploadOrderDeliveryImages;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;
internal sealed class UploadOrderDeliveryImages : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders/{id}/deliver/images", async (Guid id, IFormFileCollection images, ISender sender) =>
        {
            Result result = await sender.Send(new UploadOrderDeliveryImagesCommand(id, images));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeliverOrders)
            .WithTags(Tags.Orders)
            .DisableAntiforgery();
    }
}
