using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Categories;
public sealed class Category : Entity
{
    private Category()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public bool IsArchived { get; private set; }

    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsArchived = false
        };

        return category;
    }

    public void Archive()
    {
        IsArchived = true;
    }

    public void ChangeName(string name)
    {
        if (Name == name)
        {
            return;
        }

        Name = name;
    }
}
