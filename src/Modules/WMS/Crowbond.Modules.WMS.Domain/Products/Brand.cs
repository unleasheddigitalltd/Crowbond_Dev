using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class Brand : Entity
{
    private Brand()
    {        
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Brand Create(string name)
    {
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        brand.Raise(new BrandCreatedDomainEvent(brand.Id));
        return brand;
    }
    public void Update(string name)
    {
        Name = name;
        Raise(new BrandUpdatedDomainEvent(Id, Name));
    }
}
