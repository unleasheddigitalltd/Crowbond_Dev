﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        Error.NotFound("Users.NotFound", $"The user with the identifier {userId} not found");
    
    public static Error RoleNotFound(string roleName) =>
        Error.NotFound("Users.RoleNotFound", $"The role with the name {roleName} not found");

    public static Error NotFound(string identityId) =>
        Error.NotFound("Users.NotFound", $"The user with the IDP identifier {identityId} not found");
    
    public static Error InvalidRole(string roleName) =>
        Error.Problem("Users.InvalidRole", $"The {roleName} role cannot be assigned to a user.");

    public static Error UnhandledRole(string roleName) =>
        Error.Problem("Users.UnhandledRole", $"The {roleName} role is unhandled.");

    public static Error UserRoleAlreadyExist(string roleName) =>
        Error.Conflict("Users.UserRoleAlreadyExist", $"The {roleName} role for this user is already exist.");

    public static Error UserRoleNotExist(string roleName) =>
        Error.Conflict("Users.UserRoleNotExist", $"The {roleName} role for this user was not exist.");

    public static readonly Error DriverActiveRouteTripApiSentNoValue =
        Error.Conflict("Users.DriverActiveRouteTripApiSentNoValue", $"The driver active route trip api has sent no value.");
    public static Error DriverAlreadyLoggedOn(string routeName) =>
        Error.Conflict("Users.DriverAlreadyLoggedOn", $"The driver is already has active log on {routeName} route.");

    public static readonly Error WarehouseOperatorActiveTaskApiSentNoValue =
        Error.Conflict("Users.WarehouseOperatorActiveTaskApiSentNoValue", $"The warehouse operator active task api has sent no value.");
    public static Error WarehouseOperatorAlreadyAssigned(string taskNo) =>
        Error.Conflict("Users.WarehouseOperatorAlreadyAssigned", $"The warehouse operator is already has active assignment on {taskNo} task.");
}
