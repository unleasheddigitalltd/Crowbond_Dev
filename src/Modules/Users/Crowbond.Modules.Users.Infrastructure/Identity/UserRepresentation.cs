namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed record UserRepresentation(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    UserAttribute Attributes,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);
