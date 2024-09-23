using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Crowbond.Modules.OMS.Application.Orders.GetMyOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Orders;

internal sealed class GetMyOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/my/{id}", async (IContactContext contactContext, Guid id, ISender sender) =>
        {
            Result<OrderResponse> result = await sender.Send(new GetMyOrderQuery(contactContext.ContactId, id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetMyOrders)
        .WithTags(Tags.Orders);
    }
}
