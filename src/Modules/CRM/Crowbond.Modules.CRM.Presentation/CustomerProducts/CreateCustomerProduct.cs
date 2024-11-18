using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerProducts;

internal sealed class CreateCustomerProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{customerId}/products", async (Guid customerId, Request request, ISender sender) =>
        {
            Result result = await sender.Send(
                new CreateCustomerProductCommand(
                    customerId,
                    request.ProductId,
                    request.FixedPrice,
                    request.FixedDiscount,
                    request.Comments,
                    request.EffectiveDate,
                    request.ExpiryDate));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateCustomerProducts)
            .WithTags(Tags.Customers);

    }

    private sealed record Request(
        Guid ProductId,
        decimal? FixedPrice,
        decimal? FixedDiscount,
        string? Comments,
        DateOnly? EffectiveDate,
        DateOnly? ExpiryDate);
}
