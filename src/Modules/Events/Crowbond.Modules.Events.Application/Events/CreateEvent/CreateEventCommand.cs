using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand<Guid>;
