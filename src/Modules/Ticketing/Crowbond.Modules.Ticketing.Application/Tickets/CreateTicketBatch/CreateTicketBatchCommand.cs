using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

public sealed record CreateTicketBatchCommand(Guid OrderId) : ICommand;
