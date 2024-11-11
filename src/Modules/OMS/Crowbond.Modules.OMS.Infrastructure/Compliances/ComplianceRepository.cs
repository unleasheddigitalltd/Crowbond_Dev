using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Compliances;

internal sealed class ComplianceRepository(OmsDbContext context) : IComplianceRepository
{
    public async Task<ComplianceHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ComplianceHeaders.Include(c => c.Lines).SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<ComplianceLine?> GetLineAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ComplianceLines.Include(cl => cl.Header).SingleOrDefaultAsync(cl => cl.Id == id, cancellationToken);
    }

    public async Task<ComplianceQuestion?> GetQuestionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ComplianceQuestions.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<ComplianceQuestion>> GetActiveQuestionsAsync(CancellationToken cancellationToken = default)
    {
        return await context.ComplianceQuestions.Where(c => c.IsActive).ToListAsync(cancellationToken);
    }

    public async Task<ComplianceHeader?> GetUnSubmittedByRouteTripLogAsync(Guid routeTripLogId, CancellationToken cancellationToken = default)
    {
        return await context.ComplianceHeaders.Include(c => c.Lines).SingleOrDefaultAsync(c => c.RouteTripLogId == routeTripLogId && c.IsConfirmed == null, cancellationToken);
    }

    public async Task<Sequence?> GetSequence(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Compliance, cancellationToken);
    }

    public void InsertLine(ComplianceLine line)
    {
        context.ComplianceLines.Add(line);
    }

    public void Insert(ComplianceHeader header)
    {
        context.ComplianceHeaders.Add(header);
    }

    public void InsertQuestion(ComplianceQuestion question)
    {
        context.ComplianceQuestions.Add(question);
    }

    public void InsertLineImage(ComplianceLineImage image)
    {
        context.ComplianceLineImages.Add(image);
    }

    public async Task<ComplianceLine?> GetLineWithImagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ComplianceLines.Include(cl => cl.Header).Include(cl => cl.Images).SingleOrDefaultAsync(cl => cl.Id == id, cancellationToken);
    }

    public void RemoveLineImage(ComplianceLineImage image)
    {
        context.ComplianceLineImages.Remove(image);
    }
}
