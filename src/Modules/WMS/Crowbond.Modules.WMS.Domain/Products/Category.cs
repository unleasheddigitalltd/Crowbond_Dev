using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class Category : Entity
{
    private readonly List<ProductGroup> _productGroups = [];
    private Category()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<ProductGroup> ProductGroups => _productGroups.ToList();

    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        category.Raise(new CategoryCreatedDomainEvent(category.Id));
        return category;
    }

    public void Update(string name)
    {
        Name = name;
        Raise(new CategoryUpdatedDomainEvent(Id, Name));
    }
}
