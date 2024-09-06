using Crowbond.Common.Application.Authentication;
using Crowbond.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Common.Infrastructure.Authentication;

internal sealed class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public Guid UserId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new CrowbondException("User identifier is unavailable");
}
