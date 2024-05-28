using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.ResetUserPassword;
internal sealed class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
{
    public ResetUserPasswordCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
    }
}
