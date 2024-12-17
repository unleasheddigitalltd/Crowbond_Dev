using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.AssignRole;

internal sealed class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.RoleName).NotEmpty();
    }
}
