using Crowbond.Common.Domain;

namespace Crowbond.Modules.Warehouse.Domain.Tasks;

public sealed class TaskType : Entity
{
    public static readonly TaskType Putaway = new("Putaway");
    public TaskType(string name)
    {
        Name = name;
    }

    public TaskType()
    {        
    }

    public string Name { get; private set; }
}
