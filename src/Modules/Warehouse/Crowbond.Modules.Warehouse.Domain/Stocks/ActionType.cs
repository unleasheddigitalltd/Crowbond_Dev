using Crowbond.Common.Domain;

namespace Crowbond.Modules.Warehouse.Domain.Stocks;

public sealed class ActionType : Entity
{
    public ActionType(string name)
    {
        Name = name;
    }

    public ActionType()
    {        
    }

    public string Name { get; private set; }
}
