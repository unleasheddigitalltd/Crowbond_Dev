using Crowbond.Modules.Events.Application.Events.GetEvents;

namespace Crowbond.Modules.Events.Application.Events.SearchEvents;

public sealed record SearchEventsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<EventResponse> Events);
