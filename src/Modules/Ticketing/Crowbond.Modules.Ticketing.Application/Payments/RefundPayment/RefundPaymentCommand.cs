using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Payments.RefundPayment;

public sealed record RefundPaymentCommand(Guid PaymentId, decimal Amount) : ICommand;
