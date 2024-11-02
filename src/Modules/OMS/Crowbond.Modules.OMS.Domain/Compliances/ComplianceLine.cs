using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public sealed class ComplianceLine: Entity
{
    private ComplianceLine()
    {       
    }

    public Guid Id { get; private set; }
    public Guid ComplianceHeaderId { get; private set; }
    public Guid ComplianceQuestionId { get; private set; }
    public string? Description { get; private set; }
    public bool? Response { get; private set; }

    internal static ComplianceLine Create(Guid questionId)
    {
        var line = new ComplianceLine
        {
            Id = Guid.NewGuid(),
            ComplianceQuestionId = questionId
        };

        return line;
    }
}
