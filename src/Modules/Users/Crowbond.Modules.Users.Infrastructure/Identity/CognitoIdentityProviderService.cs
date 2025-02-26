using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Crowbond.Modules.Users.Application.Authentication;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class CognitoIdentityProviderService : IIdentityProviderService, IDisposable
{
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
    private readonly CognitoOptions _options;
    private readonly ILogger<CognitoIdentityProviderService> _logger;
    private bool _disposed;

    public CognitoIdentityProviderService(
        IOptions<CognitoOptions> options,
        ILogger<CognitoIdentityProviderService> logger)
    {
        _options = options.Value;
        _logger = logger;
        _cognitoClient = new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(_options.Region));
    }

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Attempting to register user {Username}", user.Username);
            
            var secretHash = CalculateSecretHash(user.Username);
            _logger.LogDebug("Generated secret hash for registration");

            var signUpRequest = new SignUpRequest
            {
                ClientId = _options.UserPoolClientId,
                Password = user.Password,
                Username = user.Username,
                SecretHash = secretHash,
                UserAttributes = new List<AttributeType>
                {
                    new() { Name = "email", Value = user.Email },
                    new() { Name = "given_name", Value = user.FirstName },
                    new() { Name = "family_name", Value = user.LastName }
                }
            };

            var response = await _cognitoClient.SignUpAsync(signUpRequest, cancellationToken);
            _logger.LogInformation("Successfully registered user {Username}. Auto-confirming user...", user.Username);

            // Auto-confirm the user
            var confirmRequest = new AdminConfirmSignUpRequest
            {
                UserPoolId = _options.UserPoolId,
                Username = user.Username
            };

            await _cognitoClient.AdminConfirmSignUpAsync(confirmRequest, cancellationToken);
            _logger.LogInformation("Successfully confirmed user {Username}", user.Username);
            
            return response.UserSub;
        }
        catch (UsernameExistsException ex)
        {
            _logger.LogWarning(ex, "Username {Username} already exists", user.Username);
            return Result.Failure<string>(Error.Conflict("Auth.UsernameExists", "Username already exists"));
        }
        catch (InvalidParameterException ex)
        {
            _logger.LogWarning(ex, "Invalid parameter for user {Username}: {Message}", user.Username, ex.Message);
            return Result.Failure<string>(Error.Failure("Auth.InvalidParameter", $"Invalid parameter: {ex.Message}"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register or confirm user {Username}. Error: {Message}", user.Username, ex.Message);
            return Result.Failure<string>(Error.Failure("Auth.Error", "Failed to register or confirm user"));
        }
    }

    public async Task<Result> UpdateUserAsync(string identityId, UserModel user, CancellationToken cancellationToken = default)
    {
        try
        {
            var updateRequest = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = _options.UserPoolId,
                Username = identityId,
                UserAttributes = new List<AttributeType>
                {
                    new() { Name = "email", Value = user.Email },
                    new() { Name = "given_name", Value = user.FirstName },
                    new() { Name = "family_name", Value = user.LastName },
                    new() { Name = "phone_number", Value = user.Mobile }
                }
            };

            await _cognitoClient.AdminUpdateUserAttributesAsync(updateRequest, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update user {IdentityId}", identityId);
            return Result.Failure(Error.Problem("Auth.Error", "Failed to update user"));
        }
    }

    public async Task<Result> ResetUserPasswordAsync(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var resetRequest = new AdminResetUserPasswordRequest
            {
                UserPoolId = _options.UserPoolId,
                Username = identityId
            };

            await _cognitoClient.AdminResetUserPasswordAsync(resetRequest, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset password for user {IdentityId}", identityId);
            return Result.Failure(Error.Problem("Auth.Error", "Failed to reset user password"));
        }
    }

    public async Task<Result> LogOutUserAsync(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var signOutRequest = new AdminUserGlobalSignOutRequest
            {
                UserPoolId = _options.UserPoolId,
                Username = identityId
            };

            await _cognitoClient.AdminUserGlobalSignOutAsync(signOutRequest, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to log out user {IdentityId}", identityId);
            return Result.Failure(Error.Problem("Auth.Error", "Failed to log out user"));
        }
    }

    public async Task<Result> DeleteUser(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleteRequest = new AdminDeleteUserRequest
            {
                UserPoolId = _options.UserPoolId,
                Username = identityId
            };

            await _cognitoClient.AdminDeleteUserAsync(deleteRequest, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete user {IdentityId}", identityId);
            return Result.Failure(Error.Problem("Auth.Error", "Failed to delete user"));
        }
    }

    public async Task<Result<AuthenticationResult>> AuthenticateAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Attempting to authenticate user {Username}", username);
            
            var secretHash = CalculateSecretHash(username);
            _logger.LogDebug("Generated secret hash for user {Username}", username);

            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _options.UserPoolClientId,
                AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", username},
                    {"PASSWORD", password},
                    {"SECRET_HASH", secretHash}
                }
            };

            _logger.LogDebug("Sending authentication request for user {Username} with client ID {ClientId}", 
                username, _options.UserPoolClientId);

            var response = await _cognitoClient.InitiateAuthAsync(authRequest, cancellationToken);
            
            // Get the user's sub which is needed for refresh token
            var getUserRequest = new GetUserRequest
            {
                AccessToken = response.AuthenticationResult.AccessToken
            };
            var userResponse = await _cognitoClient.GetUserAsync(getUserRequest, cancellationToken);
            var sub = userResponse.UserAttributes.Find(attr => attr.Name == "sub")?.Value;

            if (string.IsNullOrEmpty(sub))
            {
                _logger.LogWarning("Could not get sub from user attributes");
                return Result.Failure<AuthenticationResult>(Error.Failure("Auth.MissingSub", "Could not get user sub from Cognito"));
            }

            _logger.LogInformation("Successfully authenticated user {Username} with sub {Sub}", username, sub);

            // Store sub in claims
            return new AuthenticationResult(
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                response.AuthenticationResult.RefreshToken,
                response.AuthenticationResult.ExpiresIn,
                sub);
        }
        catch (NotAuthorizedException ex)
        {
            _logger.LogWarning(ex, "Invalid credentials for user {Username}. Error: {Message}", username, ex.Message);
            return Result.Failure<AuthenticationResult>(Error.Failure("Auth.InvalidCredentials", "Invalid credentials"));
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogWarning(ex, "User {Username} not found. Error: {Message}", username, ex.Message);
            return Result.Failure<AuthenticationResult>(Error.NotFound("Auth.UserNotFound", "User not found"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating user {Username}. Error: {Message}", username, ex.Message);
            return Result.Failure<AuthenticationResult>(Error.Problem("Auth.Error", "An error occurred during authentication"));
        }
    }

    public async Task<Result<AuthenticationResult>> RefreshTokenAsync(
        string refreshToken,
        string sub,
        CancellationToken cancellationToken = default)
    {
        try
        {

            var secretHash = CalculateSecretHash(sub);
            var refreshRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
                ClientId = _options.UserPoolClientId,
                AuthParameters = new Dictionary<string, string>
                {
                    {"REFRESH_TOKEN", refreshToken},
                    {"USERNAME", sub},
                    {"SECRET_HASH", secretHash}
                }
            };

            var response = await _cognitoClient.InitiateAuthAsync(refreshRequest, cancellationToken);
            
            return new AuthenticationResult(
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                refreshToken,
                response.AuthenticationResult.ExpiresIn,
                sub);
        }
        catch (NotAuthorizedException ex)
        {
            _logger.LogWarning(ex, "Invalid refresh token");
            return Result.Failure<AuthenticationResult>(Error.Failure("Auth.InvalidToken", "Invalid refresh token"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing token");
            return Result.Failure<AuthenticationResult>(Error.Problem("Auth.Error", "An error occurred while refreshing the token"));
        }
    }

    private string CalculateSecretHash(string username)
    {
        _logger.LogDebug("Calculating secret hash for username {Username}", username);
        
        var message = username + _options.UserPoolClientId;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var keyBytes = Encoding.UTF8.GetBytes(_options.UserPoolClientSecret);
        
        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(messageBytes);
        var base64Hash = Convert.ToBase64String(hashBytes);
        
        _logger.LogDebug("Secret hash calculated successfully");
        return base64Hash;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _cognitoClient?.Dispose();
            _disposed = true;
        }
    }
}
