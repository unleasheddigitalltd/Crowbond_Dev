﻿using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public interface IComplianceRepository
{
    Task<ComplianceHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ComplianceLine?> GetLineAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ComplianceQuestion?> GetQuestionAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ComplianceQuestion>> GetActiveQuestionsAsync(CancellationToken cancellationToken = default);
    Task<ComplianceHeader?> GetUnSubmittedByRouteTripLogAsync(Guid routeTripLogId, CancellationToken cancellationToken = default);
    Task<ComplianceLine?> GetLineWithImagesAsync(Guid id, CancellationToken cancellationToken= default);
    Task<Sequence?> GetSequence(CancellationToken cancellationToken = default);
    void Insert(ComplianceHeader header);
    void InsertLine(ComplianceLine line);
    void InsertLineImage(ComplianceLineImage image);
    void RemoveLineImage(ComplianceLineImage image);
    void InsertQuestion(ComplianceQuestion question);
}
