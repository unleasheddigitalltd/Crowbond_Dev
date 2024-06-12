using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class Task : Entity
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string TaskTypeName { get; private set; }

    public static Task Create(Guid userId, string taskTypeName)
    {
        var task = new Task
        {
            Id = Guid.NewGuid(),
            TaskTypeName = taskTypeName,
            UserId = userId
        };

        return task;
    }
}
