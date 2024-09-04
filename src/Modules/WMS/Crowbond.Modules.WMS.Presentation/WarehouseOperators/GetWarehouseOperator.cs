using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.WarehouseOperators.GerWarehouseOperator;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.WarehouseOperators;

internal sealed class GetWarehouseOperator : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("warehouse-operators/{id}", async (Guid id, ISender sender) =>
        {
            Result<WarehouseOperatorResponse> result = await sender.Send(new GetWarehouseOperatorQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetWarehouseOperators)
        .WithTags(Tags.WarehouseOperators);
    }
}
