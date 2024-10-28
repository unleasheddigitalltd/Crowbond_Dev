// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class OrderHeaders_Add_DueDaysForInvoice : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "payment_term",
            schema: "oms",
            table: "order_headers",
            newName: "due_date_calculation_basis");

        migrationBuilder.AddColumn<int>(
            name: "due_days_for_invoice",
            schema: "oms",
            table: "order_headers",
            type: "integer",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "due_date_calculation_basis",
            schema: "oms",
            table: "order_headers");

        migrationBuilder.RenameColumn(
            name: "due_days_for_invoice",
            schema: "oms",
            table: "order_headers",
            newName: "payment_term");
    }
}
