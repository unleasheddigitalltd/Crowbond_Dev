using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;

public sealed record UpdateTicketTypePriceCommand(Guid TicketTypeId, decimal Price) : ICommand;
