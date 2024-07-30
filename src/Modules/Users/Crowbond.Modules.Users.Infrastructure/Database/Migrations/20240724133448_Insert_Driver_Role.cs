// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_Driver_Role : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "roles",
            keyColumn: "name",
            keyValue: "Member");

        migrationBuilder.InsertData(
            schema: "users",
            table: "roles",
            column: "name",
            values: new object[]
            {
                "Customer",
                "Driver"
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "users",
            table: "roles",
            keyColumn: "name",
            keyValue: "Customer");

        migrationBuilder.DeleteData(
            schema: "users",
            table: "roles",
            keyColumn: "name",
            keyValue: "Driver");

        migrationBuilder.InsertData(
            schema: "users",
            table: "roles",
            column: "name",
            value: "Member");
    }
}
