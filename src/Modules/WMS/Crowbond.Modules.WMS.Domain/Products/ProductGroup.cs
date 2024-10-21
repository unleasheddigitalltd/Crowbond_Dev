using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class ProductGroup : Entity
{
    private readonly List<Brand> _brands = [];
    private ProductGroup()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Brand> Brands => _brands.ToList();

    public static ProductGroup Create(string name)
    {
        var productGroup = new ProductGroup
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        productGroup.Raise(new ProductGroupCreatedDomainEvent(productGroup.Id));
        return productGroup;
    }

    public void Update(string name)
    {
        Name = name;
        Raise(new ProductGroupUpdatedDomainEvent(Id, Name));
    }
}
