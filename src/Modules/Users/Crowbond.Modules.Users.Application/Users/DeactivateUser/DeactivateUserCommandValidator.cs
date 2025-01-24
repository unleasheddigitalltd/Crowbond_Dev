using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.DeactivateUser;

internal sealed class DeactivateUserCommandValidator: AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
    }
}
