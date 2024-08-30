using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;
internal sealed class TaskRepository(WmsDbContext context) : ITaskRepository
{
    public async Task<TaskHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.TaskHeaders.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Task, cancellationToken);
    }

    public void Insert(TaskHeader taskHeader)
    {
        context.TaskHeaders.Add(taskHeader);
    }

    public void AddAssignment(TaskAssignment assignment)
    {
        context.TaskAssignments.Add(assignment);
    }
}
