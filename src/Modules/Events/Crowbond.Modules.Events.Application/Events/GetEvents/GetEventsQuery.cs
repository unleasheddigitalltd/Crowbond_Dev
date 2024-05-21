using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<EventResponse>>;
