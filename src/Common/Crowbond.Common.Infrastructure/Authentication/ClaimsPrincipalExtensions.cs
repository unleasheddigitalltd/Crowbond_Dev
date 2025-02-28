using System.Security.Claims;
using Crowbond.Common.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Crowbond.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    private static readonly ILogger Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger(typeof(ClaimsPrincipalExtensions));
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.UserId)?.Value;

        Logger.LogInformation("GetUserId called. User ID from claims: {UserId}", userId);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new CrowbondException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        // For Cognito, use the sub claim which matches our stored identity_id
        return principal?.FindFirst("sub")?.Value ??
               principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new CrowbondException("User identity is unavailable");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new CrowbondException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
