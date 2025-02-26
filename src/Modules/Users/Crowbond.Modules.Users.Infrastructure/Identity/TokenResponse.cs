using System.Text.Json.Serialization;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("id_token")]
    public string IdToken { get; set; } = null!;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = null!;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("sub")]
    public string Sub { get; set; } = null!;
}
