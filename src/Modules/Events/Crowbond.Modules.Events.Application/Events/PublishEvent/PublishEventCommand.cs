using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
