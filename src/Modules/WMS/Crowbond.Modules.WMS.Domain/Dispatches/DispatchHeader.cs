using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Dispatches;

public sealed class DispatchHeader : Entity
{
    private readonly List<DispatchLine> _lines = new();

    private DispatchHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string DispatchNo { get; private set; }

    public Guid OrderId { get; private set; }

    public string OrderNo { get; private set; }

    public DispatchStatus Status { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<DispatchLine> Lines => _lines;

    public static DispatchHeader Create(
        string dispatchNo,
        Guid orderId,
        string orderNo)
    {
        var header = new DispatchHeader
        {
            Id = Guid.NewGuid(),
            DispatchNo = dispatchNo,
            OrderId = orderId,
            OrderNo = orderNo,
            Status = DispatchStatus.NotStarted
        };

        return header;
    }

    public DispatchLine AddLine(
        Guid productId,
        decimal qty)
    {
        var line = DispatchLine.Create(productId, qty);
        _lines.Add(line);
        return line;
    }
}
