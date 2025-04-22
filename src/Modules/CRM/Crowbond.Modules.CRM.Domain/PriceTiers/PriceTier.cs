using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.PriceTiers;

public sealed class PriceTier : Entity
{
    private PriceTier()
    {        
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public bool IsFallbackTier { get; private set; }

    public static PriceTier Create(string name, bool isFallbackTier = false)
    {
        var priceTier = new PriceTier
        {
            Name = name,
            IsFallbackTier = isFallbackTier
        };  

        return priceTier;
    }

    public void Update(string name)
    { 
        Name = name;
    }

    public void UpdateIsFallbackTier(bool isFallbackTier)
    {
        IsFallbackTier = isFallbackTier;
    }
}
