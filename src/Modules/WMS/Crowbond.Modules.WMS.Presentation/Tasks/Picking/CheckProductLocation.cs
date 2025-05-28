using Crowbond.Common.Application.Data;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class CheckProductLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{productId}/location-info", async (Guid productId, IDbConnectionFactory dbConnectionFactory) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            var productInfo = await connection.QuerySingleOrDefaultAsync(
                @"SELECT 
                    p.id,
                    p.name,
                    p.default_location,
                    l.name as location_name
                  FROM wms.products p
                  LEFT JOIN wms.locations l ON p.default_location = l.id
                  WHERE p.id = @productId", 
                new { productId });

            if (productInfo == null)
            {
                return Results.NotFound("Product not found");
            }

            var availableLocations = await connection.QueryAsync(
                "SELECT id, name FROM wms.locations ORDER BY name");

            return Results.Ok(new
            {
                Product = productInfo,
                AvailableLocations = availableLocations
            });
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);

        app.MapPut("products/{productId}/set-location/{locationId}", async (
            Guid productId, 
            Guid locationId, 
            IDbConnectionFactory dbConnectionFactory) =>
        {
            await using var connection = await dbConnectionFactory.OpenConnectionAsync();

            var rowsAffected = await connection.ExecuteAsync(
                "UPDATE wms.products SET default_location = @locationId WHERE id = @productId",
                new { productId, locationId });

            if (rowsAffected == 0)
            {
                return Results.NotFound("Product not found");
            }

            return Results.Ok(new { Message = "Default location updated successfully" });
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}