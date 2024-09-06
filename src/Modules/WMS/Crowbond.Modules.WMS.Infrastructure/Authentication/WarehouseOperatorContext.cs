using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.WMS.Infrastructure.Authentication;

internal sealed class WarehouseOperatorContext(IHttpContextAccessor httpContextAccessor) : IWarehouseOperatorContext
{
    public Guid WarehouseOperatorId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new CrowbondException("User identifier is unavailable");
}
