namespace Crowbond.Modules.Users.Application.Users.GetUser;

public sealed record UserResponse(Guid Id, string Username, string Email, string FirstName, string LastName, string Mobile, bool IsActive, string Roles);
