using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.CreateUser;

internal sealed class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().MaximumLength(100);
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Mobile).NotEmpty().MaximumLength(20).Matches(@"^(\+44|0)7\d{9}$");
        RuleFor(c => c.Email).EmailAddress();
    }
}
