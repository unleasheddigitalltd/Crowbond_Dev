using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Compliances.SubmitCompliance;

internal sealed class SubmitComplianceCommandValidator: AbstractValidator<SubmitComplianceCommand>
{
    public SubmitComplianceCommandValidator()
    {
        RuleFor(c => c.Temprature).NotNull().PrecisionScale(4, 2, false);
    }
}
