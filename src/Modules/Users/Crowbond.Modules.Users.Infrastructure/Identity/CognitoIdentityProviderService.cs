using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Crowbond.Modules.Users.Infrastructure.Identity;

internal sealed class CognitoIdentityProviderService : IIdentityProviderService
{
    private readonly IAmazonCognitoIdentityProvider _cognitoClient;
    private readonly CognitoOptions _options;
    private readonly ILogger<CognitoIdentityProviderService> _logger;

    public CognitoIdentityProviderService(
        IOptions<CognitoOptions> options,
        ILogger<CognitoIdentityProviderService> logger)
    {
        _options = options.Value;
        _logger = logger;
        _cognitoClient = new AmazonCognitoIdentityProviderClient(
            RegionEndpoint.GetBySystemName(_options.Region));
    }

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        try
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _options.UserPoolClientId,
                Password = user.Password,
                Username = user.Username,
                UserAttributes = new List<AttributeType>
                {
                    new() { Name = "email", Value = user.Email },
                    new() { Name = "given_name", Value = user.FirstName },
                    new() { Name = "family_name", Value = user.LastName },
                    new() { Name = "phone_number", Value = user.Mobile }
                }
            };

            var response = await _cognitoClient.SignUpAsync(signUpRequest, cancellationToken);
            return response.UserSub;
        }
        catch (UsernameExistsException)
        {
            _logger.LogWarning("Username {Username} already exists", user.Username);
            return Result.Failure("Username already exists");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register user {Username}", user.Username);
            return Result.Failure("Failed to register user");
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
            return Result.Failure("Failed to update user");
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
            return Result.Failure("Failed to reset user password");
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
            return Result.Failure("Failed to log out user");
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
            return Result.Failure("Failed to delete user");
        }
    }

    public async Task<Result<AuthenticationResult>> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _options.UserPoolClientId,
                AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", username},
                    {"PASSWORD", password}
                }
            };

            var response = await _cognitoClient.InitiateAuthAsync(authRequest, cancellationToken);

            if (response.AuthenticationResult == null)
            {
                return Result.Failure<AuthenticationResult>("Authentication failed");
            }

            var result = new AuthenticationResult(
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                response.AuthenticationResult.RefreshToken,
                response.AuthenticationResult.ExpiresIn);

            return result;
        }
        catch (NotAuthorizedException)
        {
            _logger.LogWarning("Invalid credentials for user {Username}", username);
            return Result.Failure<AuthenticationResult>("Invalid username or password");
        }
        catch (UserNotFoundException)
        {
            _logger.LogWarning("User not found {Username}", username);
            return Result.Failure<AuthenticationResult>("Invalid username or password");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to authenticate user {Username}", username);
            return Result.Failure<AuthenticationResult>("Authentication failed");
        }
    }

    public async Task<Result<AuthenticationResult>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var refreshRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
                ClientId = _options.UserPoolClientId,
                AuthParameters = new Dictionary<string, string>
                {
                    {"REFRESH_TOKEN", refreshToken}
                }
            };

            var response = await _cognitoClient.InitiateAuthAsync(refreshRequest, cancellationToken);

            if (response.AuthenticationResult == null)
            {
                return Result.Failure<AuthenticationResult>("Token refresh failed");
            }

            var result = new AuthenticationResult(
                response.AuthenticationResult.AccessToken,
                response.AuthenticationResult.IdToken,
                refreshToken, // Keep the original refresh token as Cognito doesn't return a new one
                response.AuthenticationResult.ExpiresIn);

            return result;
        }
        catch (NotAuthorizedException)
        {
            _logger.LogWarning("Invalid refresh token");
            return Result.Failure<AuthenticationResult>("Invalid refresh token");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh token");
            return Result.Failure<AuthenticationResult>("Token refresh failed");
        }
    }
}
