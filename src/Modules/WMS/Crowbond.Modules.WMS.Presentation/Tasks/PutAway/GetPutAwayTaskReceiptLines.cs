using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class GetPutAwayTaskReceiptLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/putaway/{id}/receipt-lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<TaskReceiptLineResponse>> result = await sender.Send(new GetPutAwayTaskReceiptLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
