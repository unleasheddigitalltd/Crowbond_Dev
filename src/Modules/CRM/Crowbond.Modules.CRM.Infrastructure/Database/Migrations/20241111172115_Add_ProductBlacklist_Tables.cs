// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_ProductBlacklist_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "customer_products_blacklist",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                created_by = table.Column<Guid>(type: "uuid", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                last_modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                last_modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_customer_products_blacklist", x => x.id);
                table.ForeignKey(
                    name: "fk_customer_products_blacklist_customers_customer_id",
                    column: x => x.customer_id,
                    principalSchema: "crm",
                    principalTable: "customers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_customer_products_blacklist_products_product_id",
                    column: x => x.product_id,
                    principalSchema: "crm",
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "supplier_products_blacklist",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                created_by = table.Column<Guid>(type: "uuid", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                last_modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                last_modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_supplier_products_blacklist", x => x.id);
                table.ForeignKey(
                    name: "fk_supplier_products_blacklist_products_product_id",
                    column: x => x.product_id,
                    principalSchema: "crm",
                    principalTable: "products",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_supplier_products_blacklist_suppliers_supplier_id",
                    column: x => x.supplier_id,
                    principalSchema: "crm",
                    principalTable: "suppliers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_customer_products_blacklist_customer_id",
            schema: "crm",
            table: "customer_products_blacklist",
            column: "customer_id");

        migrationBuilder.CreateIndex(
            name: "ix_customer_products_blacklist_product_id",
            schema: "crm",
            table: "customer_products_blacklist",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_blacklist_product_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_blacklist_supplier_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            column: "supplier_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "customer_products_blacklist",
            schema: "crm");

        migrationBuilder.DropTable(
            name: "supplier_products_blacklist",
            schema: "crm");
    }
}
