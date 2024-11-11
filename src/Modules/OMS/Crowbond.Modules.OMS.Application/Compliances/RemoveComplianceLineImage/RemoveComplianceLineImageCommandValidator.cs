using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Compliances.RemoveComplianceLineImage;

internal sealed class RemoveComplianceLineImageCommandValidator: AbstractValidator<RemoveComplianceLineImageCommand>
{
    public RemoveComplianceLineImageCommandValidator()
    {
        RuleFor(d => d.ImageName).NotEmpty().MaximumLength(100);
    }
}
