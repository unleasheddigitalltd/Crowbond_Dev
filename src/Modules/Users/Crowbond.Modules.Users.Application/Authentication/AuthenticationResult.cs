namespace Crowbond.Modules.Users.Application.Authentication;

public sealed class AuthenticationResult
{
    public AuthenticationResult(
        string accessToken,
        string idToken,
        string refreshToken,
        int expiresIn)
    {
        AccessToken = accessToken;
        IdToken = idToken;
        RefreshToken = refreshToken;
        ExpiresIn = expiresIn;
    }

    public string AccessToken { get; }
    public string IdToken { get; }
    public string RefreshToken { get; }
    public int ExpiresIn { get; }
}
