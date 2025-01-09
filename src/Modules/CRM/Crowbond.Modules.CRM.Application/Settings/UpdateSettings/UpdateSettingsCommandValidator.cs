using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Settings.UpdateSettings;

internal sealed class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator()
    {
        RuleFor(s => s.PaymentTerms).NotNull().IsInEnum();
    }
}
