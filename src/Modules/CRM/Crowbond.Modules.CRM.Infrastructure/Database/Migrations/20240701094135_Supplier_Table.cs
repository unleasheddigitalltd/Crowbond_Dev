using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Supplier_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "suppliers",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                account_number = table.Column<int>(type: "integer", nullable: false),
                supplier_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                address_line1 = table.Column<string>(type: "text", nullable: false),
                address_line2 = table.Column<string>(type: "text", nullable: true),
                address_town_city = table.Column<string>(type: "text", nullable: false),
                address_county = table.Column<string>(type: "text", nullable: false),
                address_country = table.Column<string>(type: "text", nullable: false),
                address_postal_code = table.Column<string>(type: "text", nullable: false),
                billing_address_line1 = table.Column<string>(type: "text", nullable: false),
                billing_address_line2 = table.Column<string>(type: "text", nullable: true),
                billing_address_town_city = table.Column<string>(type: "text", nullable: false),
                billing_address_county = table.Column<string>(type: "text", nullable: false),
                billing_address_country = table.Column<string>(type: "text", nullable: false),
                billing_address_postal_code = table.Column<string>(type: "text", nullable: false),
                email_address = table.Column<string>(type: "text", nullable: false),
                phone_number = table.Column<string>(type: "text", nullable: false),
                contact_name = table.Column<string>(type: "text", nullable: false),
                payment_terms = table.Column<int>(type: "integer", nullable: false),
                supplier_notes = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_suppliers", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "suppliers",
            schema: "crm");
    }
}
