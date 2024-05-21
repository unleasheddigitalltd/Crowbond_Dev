using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Crowbond.Modules.Ticketing.Application.Tickets.GetTicketByCode;

public sealed record GetTicketByCodeQuery(string Code) : IQuery<TicketResponse>;
