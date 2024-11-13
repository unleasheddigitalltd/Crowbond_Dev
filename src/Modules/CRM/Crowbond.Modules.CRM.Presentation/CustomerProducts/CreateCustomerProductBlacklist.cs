using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductBlacklist;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class CreateCustomerProductBlacklist : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{customerId}/product-blacklist", async (Guid customerId, Request request, ISender sender) =>
        {
            Result result = await sender.Send(
                new CreateCustomerProductBlacklistCommand(
                    customerId,
                    request.ProductId,
                    request.Comments));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateCustomerProductBlacklist)
            .WithTags(Tags.Customers);

    }

    private sealed record Request(
        Guid ProductId,
        string? Comments);
}
