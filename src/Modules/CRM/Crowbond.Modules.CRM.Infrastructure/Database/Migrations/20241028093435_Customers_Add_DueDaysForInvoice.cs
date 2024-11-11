// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Customers_Add_DueDaysForInvoice : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "payment_terms",
            schema: "crm",
            table: "suppliers");

        migrationBuilder.RenameColumn(
            name: "payment_terms",
            schema: "crm",
            table: "customers",
            newName: "due_date_calculation_basis");

        migrationBuilder.RenameColumn(
            name: "invoice_due_days",
            schema: "crm",
            table: "customers",
            newName: "due_days_for_invoice");

        migrationBuilder.RenameColumn(
            name: "custom_payment_term",
            schema: "crm",
            table: "customers",
            newName: "custom_payment_terms");

        migrationBuilder.CreateTable(
            name: "settings",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                payment_terms = table.Column<int>(type: "integer", nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_settings", x => x.id);
            });

        migrationBuilder.InsertData(
            schema: "crm",
            table: "settings",
            columns: [ "id", "deleted_by", "deleted_on_utc", "is_deleted", "payment_terms" ],
            values: new object[] { new Guid("847f725f-2110-40f8-a1b3-06ca5722cb83"), null, null, false, 1 });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "settings",
            schema: "crm");

        migrationBuilder.RenameColumn(
            name: "due_days_for_invoice",
            schema: "crm",
            table: "customers",
            newName: "payment_terms");

        migrationBuilder.RenameColumn(
            name: "due_date_calculation_basis",
            schema: "crm",
            table: "customers",
            newName: "invoice_due_days");

        migrationBuilder.RenameColumn(
            name: "custom_payment_terms",
            schema: "crm",
            table: "customers",
            newName: "custom_payment_term");

        migrationBuilder.AddColumn<int>(
            name: "payment_terms",
            schema: "crm",
            table: "suppliers",
            type: "integer",
            nullable: false,
            defaultValue: 0);
    }
}
