using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.Ticketing.Application.Tickets.GetTicket;
using Crowbond.Modules.Ticketing.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicket : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tickets/{id}", async (Guid id, ISender sender) =>
        {
            Result<TicketResponse> result = await sender.Send(new GetTicketQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetTickets)
        .WithTags(Tags.Tickets);
    }
}
