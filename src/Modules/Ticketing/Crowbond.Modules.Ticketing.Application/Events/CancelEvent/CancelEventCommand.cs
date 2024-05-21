using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
