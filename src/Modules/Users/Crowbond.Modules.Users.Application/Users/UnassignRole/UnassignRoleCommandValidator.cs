using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

internal sealed class UnassignRoleCommandValidator : AbstractValidator<UnassignRoleCommand>
{
    public UnassignRoleCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.RoleName).NotEmpty();
    }
}
