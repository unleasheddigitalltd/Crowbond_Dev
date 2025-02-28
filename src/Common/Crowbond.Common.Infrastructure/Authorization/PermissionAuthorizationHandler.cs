using Crowbond.Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Crowbond.Common.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
#pragma warning disable CA1303 // Do not pass literals as localized parameters
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        HashSet<string> permissions = context.User.GetPermissions();
        
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
#pragma warning restore CA1303
}
