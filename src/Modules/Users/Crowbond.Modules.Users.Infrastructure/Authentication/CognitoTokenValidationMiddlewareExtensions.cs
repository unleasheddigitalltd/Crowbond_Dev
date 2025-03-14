using Microsoft.AspNetCore.Builder;

namespace Crowbond.Modules.Users.Infrastructure.Authentication;

public static class CognitoTokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseCognitoTokenValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CognitoTokenValidationMiddleware>();
    }
}
