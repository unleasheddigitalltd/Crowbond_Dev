using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.PriceTiers;

public sealed class PriceTier : Entity
{
    private PriceTier()
    {        
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static PriceTier Create(string name)
    {
        var priceTier = new PriceTier
        {
            Name = name,
        };

        return priceTier;
    }

    public void Update(string name)
    { 
        Name = name;
    }
}
