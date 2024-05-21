using Crowbond.Common.Application.Authorization;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Crowbond.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
