using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Rename_ProductBlacklist_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_blacklist_customers_customer_id",
            schema: "crm",
            table: "customer_products_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_blacklist_products_product_id",
            schema: "crm",
            table: "customer_products_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_products_blacklist_products_product_id",
            schema: "crm",
            table: "supplier_products_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_products_blacklist_suppliers_supplier_id",
            schema: "crm",
            table: "supplier_products_blacklist");

        migrationBuilder.DropPrimaryKey(
            name: "pk_supplier_products_blacklist",
            schema: "crm",
            table: "supplier_products_blacklist");

        migrationBuilder.DropPrimaryKey(
            name: "pk_customer_products_blacklist",
            schema: "crm",
            table: "customer_products_blacklist");

        migrationBuilder.RenameTable(
            name: "supplier_products_blacklist",
            schema: "crm",
            newName: "supplier_product_blacklist",
            newSchema: "crm");

        migrationBuilder.RenameTable(
            name: "customer_products_blacklist",
            schema: "crm",
            newName: "customer_product_blacklist",
            newSchema: "crm");

        migrationBuilder.RenameIndex(
            name: "ix_supplier_products_blacklist_supplier_id",
            schema: "crm",
            table: "supplier_product_blacklist",
            newName: "ix_supplier_product_blacklist_supplier_id");

        migrationBuilder.RenameIndex(
            name: "ix_supplier_products_blacklist_product_id",
            schema: "crm",
            table: "supplier_product_blacklist",
            newName: "ix_supplier_product_blacklist_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_customer_products_blacklist_product_id",
            schema: "crm",
            table: "customer_product_blacklist",
            newName: "ix_customer_product_blacklist_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_customer_products_blacklist_customer_id",
            schema: "crm",
            table: "customer_product_blacklist",
            newName: "ix_customer_product_blacklist_customer_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_supplier_product_blacklist",
            schema: "crm",
            table: "supplier_product_blacklist",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_customer_product_blacklist",
            schema: "crm",
            table: "customer_product_blacklist",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_customer_product_blacklist_customers_customer_id",
            schema: "crm",
            table: "customer_product_blacklist",
            column: "customer_id",
            principalSchema: "crm",
            principalTable: "customers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_customer_product_blacklist_products_product_id",
            schema: "crm",
            table: "customer_product_blacklist",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_product_blacklist_products_product_id",
            schema: "crm",
            table: "supplier_product_blacklist",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_product_blacklist_suppliers_supplier_id",
            schema: "crm",
            table: "supplier_product_blacklist",
            column: "supplier_id",
            principalSchema: "crm",
            principalTable: "suppliers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_product_blacklist_customers_customer_id",
            schema: "crm",
            table: "customer_product_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_customer_product_blacklist_products_product_id",
            schema: "crm",
            table: "customer_product_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_product_blacklist_products_product_id",
            schema: "crm",
            table: "supplier_product_blacklist");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_product_blacklist_suppliers_supplier_id",
            schema: "crm",
            table: "supplier_product_blacklist");

        migrationBuilder.DropPrimaryKey(
            name: "pk_supplier_product_blacklist",
            schema: "crm",
            table: "supplier_product_blacklist");

        migrationBuilder.DropPrimaryKey(
            name: "pk_customer_product_blacklist",
            schema: "crm",
            table: "customer_product_blacklist");

        migrationBuilder.RenameTable(
            name: "supplier_product_blacklist",
            schema: "crm",
            newName: "supplier_products_blacklist",
            newSchema: "crm");

        migrationBuilder.RenameTable(
            name: "customer_product_blacklist",
            schema: "crm",
            newName: "customer_products_blacklist",
            newSchema: "crm");

        migrationBuilder.RenameIndex(
            name: "ix_supplier_product_blacklist_supplier_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            newName: "ix_supplier_products_blacklist_supplier_id");

        migrationBuilder.RenameIndex(
            name: "ix_supplier_product_blacklist_product_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            newName: "ix_supplier_products_blacklist_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_customer_product_blacklist_product_id",
            schema: "crm",
            table: "customer_products_blacklist",
            newName: "ix_customer_products_blacklist_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_customer_product_blacklist_customer_id",
            schema: "crm",
            table: "customer_products_blacklist",
            newName: "ix_customer_products_blacklist_customer_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_supplier_products_blacklist",
            schema: "crm",
            table: "supplier_products_blacklist",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_customer_products_blacklist",
            schema: "crm",
            table: "customer_products_blacklist",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_customer_products_blacklist_customers_customer_id",
            schema: "crm",
            table: "customer_products_blacklist",
            column: "customer_id",
            principalSchema: "crm",
            principalTable: "customers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_customer_products_blacklist_products_product_id",
            schema: "crm",
            table: "customer_products_blacklist",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_products_blacklist_products_product_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_products_blacklist_suppliers_supplier_id",
            schema: "crm",
            table: "supplier_products_blacklist",
            column: "supplier_id",
            principalSchema: "crm",
            principalTable: "suppliers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
