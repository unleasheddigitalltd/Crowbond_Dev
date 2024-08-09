using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Supplier_Product : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_products_product_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropForeignKey(
            name: "fk_product_prices_products_product_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropTable(
            name: "products",
            schema: "crm");

        migrationBuilder.DropIndex(
            name: "ix_product_prices_product_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropIndex(
            name: "ix_customer_products_product_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.AddColumn<Guid>(
            name: "category_id",
            schema: "crm",
            table: "product_prices",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<string>(
            name: "product_name",
            schema: "crm",
            table: "product_prices",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_sku",
            schema: "crm",
            table: "product_prices",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "product_prices",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "category_id",
            schema: "crm",
            table: "customer_products",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<string>(
            name: "product_name",
            schema: "crm",
            table: "customer_products",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_sku",
            schema: "crm",
            table: "customer_products",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "customer_products",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "supplier_products",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                product_sku = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                unit_of_measure_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: false),
                unit_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                tax_rate_type = table.Column<int>(type: "integer", nullable: false),
                is_default = table.Column<bool>(type: "boolean", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_supplier_products", x => x.id);
                table.ForeignKey(
                    name: "fk_supplier_products_categories_category_id",
                    column: x => x.category_id,
                    principalSchema: "crm",
                    principalTable: "categories",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_supplier_products_suppliers_supplier_id",
                    column: x => x.supplier_id,
                    principalSchema: "crm",
                    principalTable: "suppliers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_product_prices_category_id",
            schema: "crm",
            table: "product_prices",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_customer_products_category_id",
            schema: "crm",
            table: "customer_products",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_category_id",
            schema: "crm",
            table: "supplier_products",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_supplier_id",
            schema: "crm",
            table: "supplier_products",
            column: "supplier_id");

        migrationBuilder.AddForeignKey(
            name: "fk_customer_products_categories_category_id",
            schema: "crm",
            table: "customer_products",
            column: "category_id",
            principalSchema: "crm",
            principalTable: "categories",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_product_prices_categories_category_id",
            schema: "crm",
            table: "product_prices",
            column: "category_id",
            principalSchema: "crm",
            principalTable: "categories",
            principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_categories_category_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropForeignKey(
            name: "fk_product_prices_categories_category_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropTable(
            name: "supplier_products",
            schema: "crm");

        migrationBuilder.DropIndex(
            name: "ix_product_prices_category_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropIndex(
            name: "ix_customer_products_category_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "category_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "product_name",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "product_sku",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "category_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "product_name",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "product_sku",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.CreateTable(
            name: "products",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                sku = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                unit_of_measure_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
                table.ForeignKey(
                    name: "fk_products_categories_category_id",
                    column: x => x.category_id,
                    principalSchema: "crm",
                    principalTable: "categories",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_product_prices_product_id",
            schema: "crm",
            table: "product_prices",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_customer_products_product_id",
            schema: "crm",
            table: "customer_products",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_products_category_id",
            schema: "crm",
            table: "products",
            column: "category_id");

        migrationBuilder.AddForeignKey(
            name: "fk_customer_products_products_product_id",
            schema: "crm",
            table: "customer_products",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_product_prices_products_product_id",
            schema: "crm",
            table: "product_prices",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
