using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Events.Application.Abstractions.Data;
using Crowbond.Modules.Events.Domain.Events;
using Crowbond.Modules.Events.Domain.TicketTypes;

namespace Crowbond.Modules.Events.Application.TicketTypes.CreateTicketType;

internal sealed class CreateTicketTypeCommandHandler(
    IEventRepository eventRepository,
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTicketTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure<Guid>(EventErrors.NotFound(request.EventId));
        }

        var ticketType = TicketType.Create(@event, request.Name, request.Price, request.Currency, request.Quantity);

        ticketTypeRepository.Insert(ticketType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ticketType.Id;
    }
}
