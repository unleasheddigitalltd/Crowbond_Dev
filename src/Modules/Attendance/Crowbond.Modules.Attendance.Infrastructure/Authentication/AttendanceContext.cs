using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Modules.Attendance.Application.Abstractions.Authentication;
using Crowbond.Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Crowbond.Modules.Attendance.Infrastructure.Authentication;

internal sealed class AttendanceContext(IHttpContextAccessor httpContextAccessor) : IAttendanceContext
{
    public Guid AttendeeId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new CrowbondException("User identifier is unavailable");
}
