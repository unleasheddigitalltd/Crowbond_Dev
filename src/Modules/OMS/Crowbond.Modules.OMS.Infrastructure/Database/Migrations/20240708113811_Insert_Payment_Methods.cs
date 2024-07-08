using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_Payment_Methods : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "payment_method",
            schema: "oms",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_payment_method", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "purchase_order_lines",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_sku = table.Column<string>(type: "text", nullable: false),
                product_name = table.Column<string>(type: "text", nullable: false),
                unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                qty = table.Column<int>(type: "integer", nullable: false),
                sub_total = table.Column<decimal>(type: "numeric", nullable: false),
                tax = table.Column<decimal>(type: "numeric", nullable: false),
                line_total = table.Column<decimal>(type: "numeric", nullable: false),
                foc = table.Column<bool>(type: "boolean", nullable: false),
                taxable = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_purchase_order_lines", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "purchase_order_headers",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                purchase_order_no = table.Column<string>(type: "text", nullable: false),
                paid_by = table.Column<string>(type: "text", nullable: true),
                paid_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                supplier_name = table.Column<string>(type: "text", nullable: false),
                supplier_phone = table.Column<string>(type: "text", nullable: false),
                supplier_email = table.Column<string>(type: "text", nullable: false),
                supplier_contact = table.Column<string>(type: "text", nullable: false),
                purchase_order_amount = table.Column<decimal>(type: "numeric", nullable: false),
                shipping_address_company = table.Column<string>(type: "text", nullable: false),
                shipping_address_line1 = table.Column<string>(type: "text", nullable: false),
                shipping_address_line2 = table.Column<string>(type: "text", nullable: true),
                shipping_address_town_city = table.Column<string>(type: "text", nullable: false),
                shipping_address_county = table.Column<string>(type: "text", nullable: true),
                shipping_address_country = table.Column<string>(type: "text", nullable: true),
                shipping_address_postal_code = table.Column<string>(type: "text", nullable: false),
                required_date = table.Column<DateOnly>(type: "date", nullable: false),
                purchase_date = table.Column<DateOnly>(type: "date", nullable: false),
                expected_shipping_date = table.Column<DateOnly>(type: "date", nullable: false),
                supplier_reference = table.Column<string>(type: "text", nullable: true),
                purchase_order_tax = table.Column<decimal>(type: "numeric", nullable: false),
                delivery_method = table.Column<int>(type: "integer", nullable: true),
                delivery_charge = table.Column<decimal>(type: "numeric", nullable: false),
                payment_method_name = table.Column<string>(type: "character varying(100)", nullable: false),
                payment_status = table.Column<int>(type: "integer", nullable: false),
                purchase_order_notes = table.Column<string>(type: "text", nullable: true),
                sales_order_ref = table.Column<string>(type: "text", nullable: false),
                tags = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_purchase_order_headers", x => x.id);
                table.ForeignKey(
                    name: "fk_purchase_order_headers_payment_method_payment_method_name",
                    column: x => x.payment_method_name,
                    principalSchema: "oms",
                    principalTable: "payment_method",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "oms",
            table: "payment_method",
            column: "name",
            values: new object[]
            {
                "Bank Transfer",
                "COD",
                "CreditCard",
                "Invoice"
            });

        migrationBuilder.CreateIndex(
            name: "ix_purchase_order_headers_payment_method_name",
            schema: "oms",
            table: "purchase_order_headers",
            column: "payment_method_name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "purchase_order_headers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "purchase_order_lines",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "payment_method",
            schema: "oms");
    }
}
