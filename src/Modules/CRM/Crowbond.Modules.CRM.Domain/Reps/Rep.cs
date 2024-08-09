using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Reps;

public sealed class Rep : Entity
{
    private Rep()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public static Rep Create(string name)
    {
        var rep = new Rep
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        return rep;
    }

    public void Update(string name)
    {
        Name = name; 
    }
}
