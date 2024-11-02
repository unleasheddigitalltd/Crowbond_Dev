using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Compliances.UpdateCompliance;

internal sealed class UpdateComplianceCommandValidator : AbstractValidator<UpdateComplianceCommand>
{
    public UpdateComplianceCommandValidator()
    {
        RuleForEach(c => c.Compliance.ComplianceLines).ChildRules(c =>
            {
                c.RuleFor(l => l.ComplianceLineId).NotEmpty();
                c.RuleFor(l => l.Description).MaximumLength(255);
            });
    }
}
