using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Crowbond.Modules.Ticketing.Application.Tickets.GetTicketForOrder;

public sealed record GetTicketsForOrderQuery(Guid OrderId) : IQuery<IReadOnlyCollection<TicketResponse>>;
