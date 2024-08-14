// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_PurchaseNumber_Receipts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Guid>(
            name: "purchase_order_id",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AddColumn<string>(
            name: "purchase_order_no",
            schema: "wms",
            table: "receipt_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

        migrationBuilder.DropColumn(
            name: "purchase_order_number",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.AlterColumn<Guid>(
            name: "purchase_order_id",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);
    }
}
