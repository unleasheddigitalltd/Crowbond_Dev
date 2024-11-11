using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Compliances.GetComplianceLineImages;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Compliances;

internal sealed class GetComplianceLineImages : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("compliances/lines/{id}/images", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<ComplianceLineImageResponse>> result = await sender.Send(new GetComplianceLineImagesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeliverOrders)
            .WithTags(Tags.Routes);
    }
}
