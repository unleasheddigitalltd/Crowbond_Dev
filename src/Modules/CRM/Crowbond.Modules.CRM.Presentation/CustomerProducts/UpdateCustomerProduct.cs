using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class UpdateCustomerProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("customers/{customerId}/products/{productId}", async (Guid customerId, Guid productId, Request request, ISender sender) =>
        {
            Result result = await sender.Send(
                new UpdateCustomerProductCommand(
                    customerId,
                    productId,
                    request.FixedPrice,
                    request.FixedDiscount,
                    request.Comments,
                    request.EffectiveDate,
                    request.ExpiryDate));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomerProducts)
            .WithTags(Tags.Customers);

    }

    private sealed record Request(
        decimal? FixedPrice,
        decimal? FixedDiscount,
        string? Comments,
        DateOnly? EffectiveDate,
        DateOnly? ExpiryDate);
}
