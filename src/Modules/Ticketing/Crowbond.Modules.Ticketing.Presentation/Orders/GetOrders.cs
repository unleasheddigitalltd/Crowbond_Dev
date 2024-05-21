using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Ticketing.Application.Abstractions.Authentication;
using Crowbond.Modules.Ticketing.Application.Orders.GetOrders;
using Crowbond.Modules.Ticketing.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Ticketing.Presentation.Orders;

internal sealed class GetOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders", async (ICustomerContext customerContext, ISender sender) =>
        {
            Result<IReadOnlyCollection<OrderResponse>> result = await sender.Send(
                new GetOrdersQuery(customerContext.CustomerId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetOrders)
        .WithTags(Tags.Orders);
    }
}
