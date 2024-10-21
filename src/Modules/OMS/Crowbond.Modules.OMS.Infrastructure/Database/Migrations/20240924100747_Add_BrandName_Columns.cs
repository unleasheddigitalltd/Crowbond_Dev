using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_BrandName_Columns : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "foc",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "taxable",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.AddColumn<string>(
            name: "brand_name",
            schema: "oms",
            table: "purchase_order_lines",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "category_name",
            schema: "oms",
            table: "purchase_order_lines",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_group_name",
            schema: "oms",
            table: "purchase_order_lines",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "supplier_account_number",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "brand_name",
            schema: "oms",
            table: "order_lines",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "category_name",
            schema: "oms",
            table: "order_lines",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "product_group_name",
            schema: "oms",
            table: "order_lines",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "brand_name",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "category_name",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "product_group_name",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "supplier_account_number",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "brand_name",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "category_name",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "product_group_name",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.AddColumn<bool>(
            name: "foc",
            schema: "oms",
            table: "purchase_order_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "taxable",
            schema: "oms",
            table: "purchase_order_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }
}
