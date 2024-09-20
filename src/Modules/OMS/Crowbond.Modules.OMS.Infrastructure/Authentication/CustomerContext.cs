﻿using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Modules.OMS.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.OMS.Infrastructure.Authentication;
internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : IContactContext
{
    public Guid ContactId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new CrowbondException("User identifier is unavailable");
}