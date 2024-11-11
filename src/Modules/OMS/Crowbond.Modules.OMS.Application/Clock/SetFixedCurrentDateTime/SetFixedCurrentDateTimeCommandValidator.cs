using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Clock.SetFixedCurrentDateTime;

internal sealed class SetFixedCurrentDateTimeCommandValidator: AbstractValidator<SetFixedCurrentDateTimeCommand>
{
    public SetFixedCurrentDateTimeCommandValidator()
    {
        RuleFor(d => d.CurrentDateTime).NotEmpty();
    }
}
