using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

public sealed record ArchiveTicketsForEventCommand(Guid EventId) : ICommand;
