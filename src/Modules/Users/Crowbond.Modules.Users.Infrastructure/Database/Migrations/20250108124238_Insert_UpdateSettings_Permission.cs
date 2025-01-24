using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_UpdateSettings_Permission : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            schema: "users",
            table: "permissions",
            column: "code",
            value: "settings:update");

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: [ "permission_code", "role_name" ],
            values: new object[] { "settings:update", "Administrator" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "role_permissions",
            keyColumns: [ "permission_code", "role_name" ],
            keyValues: new object[] { "settings:update", "Administrator" });

        migrationBuilder.DeleteData(
            schema: "users",
            table: "permissions",
            keyColumn: "code",
            keyValue: "settings:update");
    }
}
