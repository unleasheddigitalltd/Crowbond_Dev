using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.ActivateUser;

internal sealed class ActivateUserCommandValidator: AbstractValidator<ActivateUserCommand>
{
    public ActivateUserCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
    }
}
