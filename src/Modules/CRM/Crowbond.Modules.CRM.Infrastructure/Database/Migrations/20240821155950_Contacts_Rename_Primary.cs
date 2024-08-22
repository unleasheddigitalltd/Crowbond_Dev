using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Contacts_Rename_Primary : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "primary",
            schema: "crm",
            table: "supplier_contacts",
            newName: "is_primary");

        migrationBuilder.RenameColumn(
            name: "primary",
            schema: "crm",
            table: "customer_contacts",
            newName: "is_primary");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "is_primary",
            schema: "crm",
            table: "supplier_contacts",
            newName: "primary");

        migrationBuilder.RenameColumn(
            name: "is_primary",
            schema: "crm",
            table: "customer_contacts",
            newName: "primary");
    }
}
