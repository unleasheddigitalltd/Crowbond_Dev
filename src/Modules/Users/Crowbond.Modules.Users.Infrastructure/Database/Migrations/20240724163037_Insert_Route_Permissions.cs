// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_Route_Permissions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            schema: "users",
            table: "permissions",
            column: "code",
            values: new object[]
            {
                "routes:create",
                "routes:read",
                "routes:update",
                "routetrip:create",
                "routetrip:read",
                "routetrip:update"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: [ "permission_code", "role_name" ],
            values: new object[,]
            {
                { "routes:create", "Administrator" },
                { "routes:read", "Administrator" },
                { "routes:update", "Administrator" },
                { "routetrip:create", "Administrator" },
                { "routetrip:read", "Administrator" },
                { "routetrip:read", "Driver" },
                { "routetrip:update", "Administrator" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routes:create", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routes:read", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routes:update", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routetrip:create", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routetrip:read", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routetrip:read", "Driver" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "routetrip:update", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routes:create");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routes:read");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routes:update");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routetrip:create");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routetrip:read");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "routetrip:update");
    }
}
