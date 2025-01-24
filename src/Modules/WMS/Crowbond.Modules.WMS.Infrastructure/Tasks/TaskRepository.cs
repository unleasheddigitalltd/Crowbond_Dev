using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;
internal sealed class TaskRepository(WmsDbContext context) : ITaskRepository
{
    public async Task<TaskHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.TaskHeaders.Include(t => t.Assignments).ThenInclude(a => a.Lines).SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TaskHeader>> GetByDispatchHeader(Guid dispatchHeaderId, CancellationToken cancellationToken = default)
    {
        return await context.TaskHeaders.Include(t => t.Assignments).ThenInclude(a => a.Lines).Where(t => t.DispatchId == dispatchHeaderId).ToListAsync(cancellationToken);
    }

    public async Task<TaskAssignmentLine?> GetAssignmentLineAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.TaskAssignmentLines.Include(t => t.Assignment).ThenInclude(a => a.Header).SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
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

    public void AddAssignmentLine(TaskAssignmentLine assignmentLine)
    {
        context.TaskAssignmentLines.Add(assignmentLine);
    }
}
