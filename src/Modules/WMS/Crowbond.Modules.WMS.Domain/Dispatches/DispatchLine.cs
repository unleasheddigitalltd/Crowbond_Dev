using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Domain.Dispatches;

public sealed class DispatchLine : Entity
{
    private DispatchLine()
    { }

    public Guid Id { get; private set; }

    public Guid DispatchHeaderId { get; private set; }

    public Guid OrderId { get; private set; }

    public string OrderNo { get; private set; }

    public string CustomerBusinessName { get; private set; }

    public Guid OrderLineId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal OrderedQty { get; private set; }

    public decimal PickedQty { get; private set; }

    public bool IsPicked { get; private set; }

    internal static DispatchLine Create(
        Guid orderId,
        string orderNo,
        string customerBusinessName,
        Guid orderLineId,
        Guid productId,
        decimal orderedQty)
    {
        var line = new DispatchLine
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            OrderNo = orderNo,
            CustomerBusinessName = customerBusinessName,
            OrderLineId = orderLineId,
            ProductId = productId,
            OrderedQty = orderedQty,
            PickedQty = 0,
            IsPicked = false
        };

        return line;
    }

    internal Result Pick(decimal Qty)
    {
        if (IsPicked)
        {
            return Result.Failure(DispatchErrors.LineAlreadyPicked(Id));
        }

        PickedQty += Qty;
        IsPicked = PickedQty >= OrderedQty;

        return Result.Success();
    }

    
    internal Result FinalizePiking()
    {
        if (OrderedQty != PickedQty)
        {
            return Result.Failure(DispatchErrors.QuantityMismatch);
        }

        IsPicked = true;

        return Result.Success();
    }
}
