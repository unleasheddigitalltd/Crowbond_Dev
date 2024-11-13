using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProductBlacklist;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class UpdateCustomerProductBlacklist : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("customers/{customerId}/product-blacklist/{productId}", async (Guid customerId, Guid productId, Request request, ISender sender) =>
        {
            Result result = await sender.Send(
                new UpdateCustomerProductBlacklistCommand(
                    customerId,
                    productId,
                    request.Comments));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomerProductBlacklist)
            .WithTags(Tags.Customers);

    }

    private sealed record Request(string? Comments);
}
