using System.Security.Claims;
using Crowbond.Common.Application.Authorization;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Application.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Crowbond.Common.Infrastructure.Authorization;

internal sealed class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identityId = principal.GetIdentityId();

        if (principal.HasClaim(c => c.Type == CustomClaims.Sub))
        {
            return principal;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var result = await permissionService.GetUserPermissionsAsync(identityId);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(IPermissionService.GetUserPermissionsAsync), result.Error);
        }

        var claimsIdentity = new ClaimsIdentity(
            principal.Identity as ClaimsIdentity ?? 
            new ClaimsIdentity(principal.Identity?.AuthenticationType ?? "Bearer"));

        foreach (var claim in principal.Claims.Where(c => c.Type != CustomClaims.Permission))
        {
            claimsIdentity.AddClaim(claim);
        }

        claimsIdentity.AddClaim(new Claim(CustomClaims.Sub, identityId));

        foreach (string permission in result.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);
        
        return principal;
    }
#pragma warning restore CA1303
}
