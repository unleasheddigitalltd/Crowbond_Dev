using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;

public sealed record RefundPaymentsForEventCommand(Guid EventId) : ICommand;
