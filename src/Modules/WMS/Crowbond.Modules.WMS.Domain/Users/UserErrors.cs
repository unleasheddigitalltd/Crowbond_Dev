﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Users;

public static class UserErrors
{

    public static Error NotFound(Guid userId) =>
        Error.NotFound("Users.NotFound", $"The user with the identifier {userId} not found");
}
