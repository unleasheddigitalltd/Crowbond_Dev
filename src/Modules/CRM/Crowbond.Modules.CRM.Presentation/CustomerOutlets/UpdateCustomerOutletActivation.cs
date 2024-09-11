using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutletActivation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.CustomerOutlets;

internal sealed class UpdateCustomerOutletActivation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/outlets/{id}/activation/{isActive}", async (Guid id, bool isActive, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerOutletActivationCommand(id, isActive));

            return result.Match(Results.NoContent, ApiResults.Problem);
        }
        )
        .RequireAuthorization(Permissions.ModifyCustomerOutlets)
        .WithTags(Tags.Customers);
    }
}
