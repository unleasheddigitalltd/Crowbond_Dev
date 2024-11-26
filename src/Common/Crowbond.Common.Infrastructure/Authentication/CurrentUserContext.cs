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
            // If HttpContext is available, return the user ID, else return the system user ID
            if (httpContextAccessor.HttpContext?.User != null)
            {
                return httpContextAccessor.HttpContext.User.GetUserId();
            }

            // If no HttpContext (e.g., in a background job), return a default system user ID
            return _systemUserId;
        }
    }
}
