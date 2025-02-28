using System.Text.Json.Serialization;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class TokenRequest
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = null!;
}
