// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Suppliers_Add_IsActive : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "create_by",
            schema: "crm",
            table: "suppliers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "create_date",
            schema: "crm",
            table: "suppliers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "is_active",
            schema: "crm",
            table: "suppliers",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "crm",
            table: "suppliers",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_date",
            schema: "crm",
            table: "suppliers",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "create_by",
            schema: "crm",
            table: "suppliers");

        migrationBuilder.DropColumn(
            name: "create_date",
            schema: "crm",
            table: "suppliers");

        migrationBuilder.DropColumn(
            name: "is_active",
            schema: "crm",
            table: "suppliers");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "crm",
            table: "suppliers");

        migrationBuilder.DropColumn(
            name: "last_modified_date",
            schema: "crm",
            table: "suppliers");
    }
}
