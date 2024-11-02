using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Compliances.CreateCompliance;

internal sealed class CreateComplianceCommandValidator: AbstractValidator<CreateComplianceCommand>
{
    public CreateComplianceCommandValidator()
    {
        RuleFor(c => c.VehicleId).NotEmpty();
    }
}
