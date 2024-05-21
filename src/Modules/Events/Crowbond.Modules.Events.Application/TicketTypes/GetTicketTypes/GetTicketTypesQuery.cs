using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Events.Application.TicketTypes.GetTicketType;

namespace Crowbond.Modules.Events.Application.TicketTypes.GetTicketTypes;

public sealed record GetTicketTypesQuery(Guid EventId) : IQuery<IReadOnlyCollection<TicketTypeResponse>>;
