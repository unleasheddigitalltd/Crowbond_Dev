using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public sealed class Category : Entity
{
    private Category()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Category Create(Guid id, string name)
    {
        var category = new Category
        {
            Id = id,
            Name = name
        };

        return category;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
