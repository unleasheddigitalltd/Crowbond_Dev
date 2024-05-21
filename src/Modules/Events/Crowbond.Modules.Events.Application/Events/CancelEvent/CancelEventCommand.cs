using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
