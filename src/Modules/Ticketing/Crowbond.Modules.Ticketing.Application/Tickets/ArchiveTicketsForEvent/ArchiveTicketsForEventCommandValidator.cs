using FluentValidation;

namespace Crowbond.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class ArchiveTicketsForEventCommandValidator : AbstractValidator<ArchiveTicketsForEventCommand>
{
    public ArchiveTicketsForEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
