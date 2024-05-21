using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Ticketing.Application.Abstractions.Payments;
using Crowbond.Modules.Ticketing.Domain.Payments;

namespace Crowbond.Modules.Ticketing.Application.Payments.RefundPayment;

internal sealed class PaymentRefundedDomainEventHandler(IPaymentService paymentService)
    : DomainEventHandler<PaymentRefundedDomainEvent>
{
    public override async Task Handle(
        PaymentRefundedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await paymentService.RefundAsync(domainEvent.TransactionId, domainEvent.RefundAmount);
    }
}
