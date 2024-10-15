using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public sealed class ProductGroup : Entity
{
    private ProductGroup()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static ProductGroup Create(Guid id, string name)
    {
        var productGroup = new ProductGroup
        {
            Id = id,
            Name = name
        };

        return productGroup;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
