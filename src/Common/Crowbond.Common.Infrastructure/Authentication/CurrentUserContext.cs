using Crowbond.Common.Application.Authentication;
using Crowbond.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Common.Infrastructure.Authentication;

internal sealed class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    private static readonly Guid _systemUserId = Guid.Empty;
    public Guid UserId
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;

            // If we have a user *and* they’re authenticated, return their ID
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.GetUserId();
            }

            // Otherwise (no context, or not authenticated), treat as "system"
            return _systemUserId;
        }
    }
}
