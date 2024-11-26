using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderLineReject: Entity
{
    private OrderLineReject()
    {        
    }

    public Guid Id { get; private set; }

    public Guid OrderLineId { get; private set; }

    public decimal Qty { get; private set; }

    public Guid RejectReasonId { get; private set; }

    public string? Comments { get; private set; }

    internal static OrderLineReject Create (decimal qty, Guid rejectReason, string? comments)
    {
        var reject = new OrderLineReject
        {
            Id = Guid.NewGuid(),
            Qty = qty,
            RejectReasonId = rejectReason,
            Comments = comments
        };

        return reject;
    }
}
