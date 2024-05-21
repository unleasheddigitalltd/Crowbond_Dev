using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Modules.Ticketing.Application.Abstractions.Authentication;
using Crowbond.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
    public Guid CustomerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new CrowbondException("User identifier is unavailable");
}
