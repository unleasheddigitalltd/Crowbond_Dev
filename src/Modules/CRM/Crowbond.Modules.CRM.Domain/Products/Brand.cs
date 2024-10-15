using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;
public sealed class Brand: Entity
{
    private Brand()
    {        
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Brand Create(Guid id, string name)
    {
        var brand = new Brand
        {
            Id = id,
            Name = name
        };

        return brand;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
