using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Crowbond.Modules.Users.Application.Users.LogOutUser;
internal sealed class LogOutUserCommandValidator : AbstractValidator<LogOutUserCommand>
{
    public LogOutUserCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty();
    }
}
