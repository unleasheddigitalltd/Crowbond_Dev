// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_Picking_Permissions : Migration
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
                "tasks:picking:manage",
                "tasks:picking:read",
                "tasks:picking:update"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: [ "permission_code", "role_name" ],
            values: new object[,]
            {
                { "tasks:picking:execute", "Administrator" },
                { "tasks:picking:manage", "Administrator" },
                { "tasks:picking:read", "Administrator" },
                { "tasks:picking:read", "WarehouseOperator" },
                { "tasks:picking:update", "Administrator" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:picking:execute", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:picking:manage", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:picking:read", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:picking:read", "WarehouseOperator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:picking:update", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:picking:manage");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:picking:read");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:picking:update");
    }
}
