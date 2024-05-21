using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Tickets.GetTicket;

public sealed record GetTicketQuery(Guid TicketId) : IQuery<TicketResponse>;
