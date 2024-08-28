using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskHeader : Entity
{
    private TaskHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string TaskNo { get; private set; }

    public Guid EntityId { get; private set; }

    public TaskType TaskType { get; private set; }

    public static TaskHeader Create(
        string taskNo,
        Guid entityId,
        TaskType taskType)
    {
        var task = new TaskHeader
        {
            Id = Guid.NewGuid(),
            TaskNo = taskNo,
            EntityId = entityId,
            TaskType = taskType
        };

        return task;
    }
}
