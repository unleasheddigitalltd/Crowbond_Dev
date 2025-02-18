﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Mobile)
    : ICommand<Guid>;
