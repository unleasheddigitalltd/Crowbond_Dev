﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Brands_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_products_categories_category_id",
            schema: "wms",
            table: "products");

        migrationBuilder.DropIndex(
            name: "ix_products_category_id",
            schema: "wms",
            table: "products");

        migrationBuilder.DropPrimaryKey(
            name: "pk_categories",
            schema: "wms",
            table: "categories");

        migrationBuilder.DropColumn(
            name: "category_id",
            schema: "wms",
            table: "products");

        migrationBuilder.DropColumn(
            name: "id",
            schema: "wms",
            table: "categories");

        migrationBuilder.DropColumn(
            name: "is_archived",
            schema: "wms",
            table: "categories");

        migrationBuilder.AddColumn<string>(
            name: "brand_name",
            schema: "wms",
            table: "products",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "category_name",
            schema: "wms",
            table: "products",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_group_name",
            schema: "wms",
            table: "products",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<string>(
            name: "name",
            schema: "wms",
            table: "categories",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AddPrimaryKey(
            name: "pk_categories",
            schema: "wms",
            table: "categories",
            column: "name");

        migrationBuilder.CreateTable(
            name: "brands",
            schema: "wms",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_brands", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "product_groups",
            schema: "wms",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_product_groups", x => x.name);
            });

        migrationBuilder.CreateIndex(
            name: "ix_products_brand_name",
            schema: "wms",
            table: "products",
            column: "brand_name");

        migrationBuilder.CreateIndex(
            name: "ix_products_category_name",
            schema: "wms",
            table: "products",
            column: "category_name");

        migrationBuilder.CreateIndex(
            name: "ix_products_product_group_name",
            schema: "wms",
            table: "products",
            column: "product_group_name");

        migrationBuilder.AddForeignKey(
            name: "fk_products_brands_brand_name",
            schema: "wms",
            table: "products",
            column: "brand_name",
            principalSchema: "wms",
            principalTable: "brands",
            principalColumn: "name");

        migrationBuilder.AddForeignKey(
            name: "fk_products_categories_category_name",
            schema: "wms",
            table: "products",
            column: "category_name",
            principalSchema: "wms",
            principalTable: "categories",
            principalColumn: "name");

        migrationBuilder.AddForeignKey(
            name: "fk_products_product_groups_product_group_name",
            schema: "wms",
            table: "products",
            column: "product_group_name",
            principalSchema: "wms",
            principalTable: "product_groups",
            principalColumn: "name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_products_brands_brand_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropForeignKey(
            name: "fk_products_categories_category_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropForeignKey(
            name: "fk_products_product_groups_product_group_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropTable(
            name: "brands",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "product_groups",
            schema: "wms");

        migrationBuilder.DropIndex(
            name: "ix_products_brand_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropIndex(
            name: "ix_products_category_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropIndex(
            name: "ix_products_product_group_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropPrimaryKey(
            name: "pk_categories",
            schema: "wms",
            table: "categories");

        migrationBuilder.DropColumn(
            name: "brand_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropColumn(
            name: "category_name",
            schema: "wms",
            table: "products");

        migrationBuilder.DropColumn(
            name: "product_group_name",
            schema: "wms",
            table: "products");

        migrationBuilder.AddColumn<Guid>(
            name: "category_id",
            schema: "wms",
            table: "products",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AlterColumn<string>(
            name: "name",
            schema: "wms",
            table: "categories",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(50)",
            oldMaxLength: 50);

        migrationBuilder.AddColumn<Guid>(
            name: "id",
            schema: "wms",
            table: "categories",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<bool>(
            name: "is_archived",
            schema: "wms",
            table: "categories",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddPrimaryKey(
            name: "pk_categories",
            schema: "wms",
            table: "categories",
            column: "id");

        migrationBuilder.CreateIndex(
            name: "ix_products_category_id",
            schema: "wms",
            table: "products",
            column: "category_id");

        migrationBuilder.AddForeignKey(
            name: "fk_products_categories_category_id",
            schema: "wms",
            table: "products",
            column: "category_id",
            principalSchema: "wms",
            principalTable: "categories",
            principalColumn: "id");
    }
}
