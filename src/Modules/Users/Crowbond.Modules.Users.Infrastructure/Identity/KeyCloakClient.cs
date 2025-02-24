using System.Net.Http.Json;
using System.Text.Json;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }

    internal async Task ResetUserPasswordAsync(string userId, CancellationToken cancellationToken = default)
    {
        string[] action = ["UPDATE_PASSWORD"];

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(
            $"users/{userId}/execute-actions-email",
            action,
            new JsonSerializerOptions(JsonSerializerDefaults.Web),
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }

    internal async Task LogOutUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            $"users/{userId}/logout",
            new JsonSerializerOptions(JsonSerializerDefaults.Web),
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }

    internal async Task<TokenResponse> GetTokenAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var tokenRequest = new TokenRequest
        {
            Username = username,
            Password = password,
            GrantType = "password"
        };

        var response = await httpClient.PostAsJsonAsync("protocol/openid-connect/token", tokenRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize token response");
    }

    internal async Task<TokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenRequest = new TokenRequest
        {
            RefreshToken = refreshToken,
            GrantType = "refresh_token"
        };

        var response = await httpClient.PostAsJsonAsync("protocol/openid-connect/token", tokenRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize token response");
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return identityId;
    }

    internal async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(
            $"users/{userId}",
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
    
    internal async Task UpdateUserAsync(string userId, UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(
            $"users/{userId}",
            user,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}
