using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.WarehouseOperators;

internal sealed class CreateWarehouseOperator : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("warehouse-operators", async (WarehouseOperatorRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateWarehouseOperatorCommand(Guid.NewGuid(), request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateWarehouseOperators)
        .WithTags(Tags.WarehouseOperators);
    }
}
