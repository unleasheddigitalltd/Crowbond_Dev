using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class CompletePutAwayReceiptLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("tasks/putaway/{id}/receipt-lines/{receiptLineId}/complete", async (
            IWarehouseOperatorContext operatorContext,
            Guid id, 
            Guid receiptLineId, 
            ISender sender) =>
        {
            Result result = await sender.Send(new CompletePutAwayReceiptLineCommand(operatorContext.WarehouseOperatorId, id, receiptLineId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
