using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.SupplierProducts.UpdateSupplierProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.SupplierProducts;

internal sealed class UpdateSupplierProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("suppliers/{supplierId}/products", async (
            ClaimsPrincipal claims,
            Guid supplierId,
            IReadOnlyCollection<SupplierProductRequest> request,
            ISender sender) =>
        {
            Result result = await sender.Send(new UpdateSupplierProductsCommand(claims.GetUserId(), supplierId, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifySupplierProducts)
            .WithTags(Tags.Suppliers);
    }
}
