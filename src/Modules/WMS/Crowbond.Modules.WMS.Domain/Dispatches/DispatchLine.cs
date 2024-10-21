using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Dispatches;

public sealed class DispatchLine : Entity
{
    private DispatchLine()
    { }

    public Guid Id { get; private set; }

    public Guid DispatchHeaderId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal Qty { get; private set; }

    internal static DispatchLine Create(
        Guid productId,
        decimal qty)
    {
        var line = new DispatchLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Qty = qty
        };

        return line;
    }
}
