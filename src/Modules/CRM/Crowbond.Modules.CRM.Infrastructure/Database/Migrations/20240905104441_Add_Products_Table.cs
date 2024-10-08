﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Products_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_categories_category_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropForeignKey(
            name: "fk_product_prices_categories_category_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_products_categories_category_id",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropTable(
            name: "categories",
            schema: "crm");

        migrationBuilder.DropIndex(
            name: "ix_supplier_products_category_id",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropIndex(
            name: "ix_product_prices_category_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropIndex(
            name: "ix_customer_products_category_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "product_name",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "product_sku",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "tax_rate_type",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "supplier_products");

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

        migrationBuilder.RenameColumn(
            name: "category_id",
            schema: "crm",
            table: "supplier_products",
            newName: "created_by");

        migrationBuilder.AddColumn<DateTime>(
            name: "created_on_utc",
            schema: "crm",
            table: "supplier_products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "deleted_on_utc",
            schema: "crm",
            table: "supplier_products",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "is_deleted",
            schema: "crm",
            table: "supplier_products",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "crm",
            table: "supplier_products",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_on_utc",
            schema: "crm",
            table: "supplier_products",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "products",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                sku = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                filter_type_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                unit_of_measure_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                inventory_type_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: false),
                category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                tax_rate_type = table.Column<int>(type: "integer", nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_product_id",
            schema: "crm",
            table: "supplier_products",
            column: "product_id");

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
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_products_products_product_id",
            schema: "crm",
            table: "supplier_products",
            column: "product_id",
            principalSchema: "crm",
            principalTable: "products",
            principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_products_products_product_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropForeignKey(
            name: "fk_product_prices_products_product_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropForeignKey(
            name: "fk_supplier_products_products_product_id",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropTable(
            name: "products",
            schema: "crm");

        migrationBuilder.DropIndex(
            name: "ix_supplier_products_product_id",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropIndex(
            name: "ix_product_prices_product_id",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropIndex(
            name: "ix_customer_products_product_id",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "created_on_utc",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "deleted_on_utc",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "is_deleted",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.DropColumn(
            name: "last_modified_on_utc",
            schema: "crm",
            table: "supplier_products");

        migrationBuilder.RenameColumn(
            name: "created_by",
            schema: "crm",
            table: "supplier_products",
            newName: "category_id");

        migrationBuilder.AddColumn<string>(
            name: "product_name",
            schema: "crm",
            table: "supplier_products",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_sku",
            schema: "crm",
            table: "supplier_products",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "tax_rate_type",
            schema: "crm",
            table: "supplier_products",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "unit_of_measure_name",
            schema: "crm",
            table: "supplier_products",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "category_id",
            schema: "crm",
            table: "product_prices",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
            name: "categories",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                is_archived = table.Column<bool>(type: "boolean", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_categories", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_supplier_products_category_id",
            schema: "crm",
            table: "supplier_products",
            column: "category_id");

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

        migrationBuilder.AddForeignKey(
            name: "fk_supplier_products_categories_category_id",
            schema: "crm",
            table: "supplier_products",
            column: "category_id",
            principalSchema: "crm",
            principalTable: "categories",
            principalColumn: "id");
    }
}
