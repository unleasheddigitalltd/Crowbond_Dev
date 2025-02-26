namespace Crowbond.Common.Application.Users;

public sealed record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Mobile,
    bool IsActive,
    string Roles);
