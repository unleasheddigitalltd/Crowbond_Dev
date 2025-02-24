namespace Crowbond.Modules.Users.Application.Authentication;

public sealed record AuthenticationResult(
    string AccessToken,
    string IdToken,
    string RefreshToken,
    int ExpiresIn);
