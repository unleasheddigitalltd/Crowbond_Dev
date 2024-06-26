using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Database : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "crm");

        migrationBuilder.CreateTable(
            name: "customers",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                account_number = table.Column<int>(type: "integer", nullable: false),
                company_name = table.Column<string>(type: "text", nullable: false),
                driver_code = table.Column<string>(type: "text", nullable: true),
                shipping_address_line1 = table.Column<string>(type: "text", nullable: false),
                shipping_address_line2 = table.Column<string>(type: "text", nullable: true),
                shipping_town_city = table.Column<string>(type: "text", nullable: false),
                shipping_county = table.Column<string>(type: "text", nullable: false),
                shipping_country = table.Column<string>(type: "text", nullable: true),
                shipping_postal_code = table.Column<string>(type: "text", nullable: false),
                billing_address_line1 = table.Column<string>(type: "text", nullable: false),
                billing_address_line2 = table.Column<string>(type: "text", nullable: true),
                billing_town_city = table.Column<string>(type: "text", nullable: false),
                billing_county = table.Column<string>(type: "text", nullable: false),
                billing_country = table.Column<string>(type: "text", nullable: false),
                billing_postal_code = table.Column<string>(type: "text", nullable: false),
                price_group_id = table.Column<int>(type: "integer", nullable: false),
                invoice_period_id = table.Column<Guid>(type: "uuid", nullable: false),
                payment_terms = table.Column<int>(type: "integer", nullable: false),
                detailed_invoice = table.Column<bool>(type: "boolean", nullable: false),
                customer_notes = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_customers", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "customers",
            schema: "crm");
    }
}
