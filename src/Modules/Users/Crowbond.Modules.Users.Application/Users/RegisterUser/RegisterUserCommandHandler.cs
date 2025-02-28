using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Abstractions.Data;
using Crowbond.Modules.Users.Application.Abstractions.Identity;
using Crowbond.Modules.Users.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<RegisterUserCommandHandler> logger)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        string? cognitoUserId = null;
        
        try
        {
            var result = await identityProviderService.RegisterUserAsync(
                new UserModel(request.Username, request.Email, request.Password, request.FirstName, request.LastName, request.Mobile, true),
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            cognitoUserId = result.Value;

            var user = User.Create(
                Guid.NewGuid(),
                request.Username,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Mobile,
                cognitoUserId);

            user.AddRole(Role.Administrator);

            userRepository.Insert(user);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return user.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to save user {Username} to database. Cleaning up Cognito user", request.Username);
                
                try
                {
                    await identityProviderService.DeleteUser(cognitoUserId, cancellationToken);
                }
                catch (Exception cleanupEx)
                {
                    logger.LogError(cleanupEx, "Failed to clean up Cognito user {CognitoUserId} after registration failure", cognitoUserId);
                }
                
                return Result.Failure<Guid>(Error.Failure(
                    "Users.Registration.DatabaseError",
                    $"Failed to save user {request.Username} to database: {ex.Message}"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to register user {Username} in Cognito", request.Username);
            return Result.Failure<Guid>(Error.Failure(
                "Users.Registration.CognitoError",
                $"Failed to register user {request.Username} in Cognito: {ex.Message}"));
        }
    }
}
