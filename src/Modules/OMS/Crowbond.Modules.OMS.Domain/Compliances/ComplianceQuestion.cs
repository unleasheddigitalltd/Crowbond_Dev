using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public sealed class ComplianceQuestion: Entity
{
    private ComplianceQuestion()
    {        
    }

    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public bool IsActive { get; private set; }

    public static ComplianceQuestion Create(string text)
    {
        var question = new ComplianceQuestion
        {
            Id = Guid.NewGuid(),
            Text = text,
            IsActive = true
        };
        return question;
    }

    public void Update(string text)
    {
        Text = text;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
