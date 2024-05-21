using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
