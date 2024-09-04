using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class StockTransactionReason : Entity
{
    private StockTransactionReason()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string ActionTypeName { get; private set; }

    public static StockTransactionReason Create(string name, string actionTypeName)
    {
        var stockTransactionReason = new StockTransactionReason
        {
            Id = Guid.NewGuid(),
            Name = name,
            ActionTypeName = actionTypeName
        };

        return stockTransactionReason;
    }
}
