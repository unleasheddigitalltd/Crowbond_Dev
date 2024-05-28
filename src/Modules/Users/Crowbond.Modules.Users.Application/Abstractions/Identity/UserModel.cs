namespace Crowbond.Modules.Users.Application.Abstractions.Identity;

public sealed record UserModel(string Username, string Email, string Password, string FirstName, string LastName);
