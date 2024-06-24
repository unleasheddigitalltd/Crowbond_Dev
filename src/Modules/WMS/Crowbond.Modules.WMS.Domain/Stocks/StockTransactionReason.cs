using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class StockTransactionReason : Entity
{
    public StockTransactionReason()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string ActionTypeName { get; private set; }
}
