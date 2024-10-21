using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Customers.UploadCustomerLogo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Customers;

internal sealed class UploadCustomerLogo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers/{id}/logo", async (Guid id, IFormFile logo, ISender sender) =>
        {
            Result result = await sender.Send(new UploadCustomerLogoCommand(id, logo));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyCustomers)
            .WithTags(Tags.Customers)
            .DisableAntiforgery();
    }
}
