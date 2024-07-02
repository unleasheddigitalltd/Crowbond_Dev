using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.CRM.Presentation.Customers;

internal sealed class UpdateCustomer: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("customers/{id}", async (Guid id, CustomerDto request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCustomerCommand(id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyCustomers)
        .WithTags(Tags.Customers);
    }
}
