using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class ActionType : Entity
{    
    public static readonly ActionType Adjustment = new("Adjustment");
    public static readonly ActionType Relocating = new("Relocating");
    public static readonly ActionType PutAway = new("PutAway");

    public ActionType(string name)
    {
        Name = name;
    }

    public ActionType()
    {
    }

    public string Name { get; private set; }
}
