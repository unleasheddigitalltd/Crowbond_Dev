using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_CheckingTask_Permissions : Migration
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
                "tasks:checking:execute",
                "tasks:checking:manage",
                "tasks:checking:read",
                "tasks:checking:update"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: [ "permission_code", "role_name" ],
            values: new object[,]
            {
                { "tasks:checking:execute", "Administrator" },
                { "tasks:checking:manage", "Administrator" },
                { "tasks:checking:read", "Administrator" },
                { "tasks:checking:update", "Administrator" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:checking:execute", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:checking:manage", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:checking:read", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "tasks:checking:update", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:checking:execute");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:checking:manage");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:checking:read");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "tasks:checking:update");
    }
}
