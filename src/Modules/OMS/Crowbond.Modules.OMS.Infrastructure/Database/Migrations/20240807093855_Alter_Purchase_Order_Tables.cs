// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Alter_Purchase_Order_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "location_name",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_address_country",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_address_county",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_address_postal_code",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_address_town_city",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "supplier_contact",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "supplier_email",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "supplier_phone",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.RenameColumn(
            name: "purchase_order_id",
            schema: "oms",
            table: "purchase_order_lines",
            newName: "purchase_order_header_id");

        migrationBuilder.RenameIndex(
            name: "ix_purchase_order_lines_purchase_order_id",
            schema: "oms",
            table: "purchase_order_lines",
            newName: "ix_purchase_order_lines_purchase_order_header_id");

        migrationBuilder.AlterColumn<decimal>(
            name: "qty",
            schema: "oms",
            table: "purchase_order_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AddColumn<string>(
            name: "comments",
            schema: "oms",
            table: "purchase_order_lines",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "unit_of_measure_name",
            schema: "oms",
            table: "purchase_order_lines",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<string>(
            name: "tags",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "supplier_reference",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "supplier_name",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "shipping_address_line2",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "shipping_address_line1",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "sales_order_ref",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<decimal>(
            name: "purchase_order_tax",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<string>(
            name: "purchase_order_notes",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(500)",
            maxLength: 500,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "purchase_order_no",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<decimal>(
            name: "purchase_order_amount",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AlterColumn<DateOnly>(
            name: "purchase_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date");

        migrationBuilder.AlterColumn<string>(
            name: "paid_by",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "expected_shipping_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date");

        migrationBuilder.AlterColumn<decimal>(
            name: "delivery_charge",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric");

        migrationBuilder.AddColumn<string>(
            name: "contact_email",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "contact_full_name",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "contact_phone",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "create_by",
            schema: "oms",
            table: "purchase_order_headers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "create_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "oms",
            table: "purchase_order_headers",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "shipping_country",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "shipping_county",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "shipping_location_name",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "shipping_postal_code",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "shipping_town_city",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "status",
            schema: "oms",
            table: "purchase_order_headers",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "purchase_order_status_histories",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                purchase_order_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                changed_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_purchase_order_status_histories", x => x.id);
                table.ForeignKey(
                    name: "fk_purchase_order_status_histories_purchase_order_headers_purc",
                    column: x => x.purchase_order_header_id,
                    principalSchema: "oms",
                    principalTable: "purchase_order_headers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_purchase_order_status_histories_purchase_order_header_id",
            schema: "oms",
            table: "purchase_order_status_histories",
            column: "purchase_order_header_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "purchase_order_status_histories",
            schema: "oms");

        migrationBuilder.DropColumn(
            name: "comments",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "unit_of_measure_name",
            schema: "oms",
            table: "purchase_order_lines");

        migrationBuilder.DropColumn(
            name: "contact_email",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "contact_full_name",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "contact_phone",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "create_by",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "create_date",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "last_modified_date",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_country",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_county",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_location_name",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_postal_code",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "shipping_town_city",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.DropColumn(
            name: "status",
            schema: "oms",
            table: "purchase_order_headers");

        migrationBuilder.RenameColumn(
            name: "purchase_order_header_id",
            schema: "oms",
            table: "purchase_order_lines",
            newName: "purchase_order_id");

        migrationBuilder.RenameIndex(
            name: "ix_purchase_order_lines_purchase_order_header_id",
            schema: "oms",
            table: "purchase_order_lines",
            newName: "ix_purchase_order_lines_purchase_order_id");

        migrationBuilder.AlterColumn<int>(
            name: "qty",
            schema: "oms",
            table: "purchase_order_lines",
            type: "integer",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AlterColumn<string>(
            name: "tags",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255);

        migrationBuilder.AlterColumn<string>(
            name: "supplier_reference",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "supplier_name",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "shipping_address_line2",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "shipping_address_line1",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255);

        migrationBuilder.AlterColumn<string>(
            name: "sales_order_ref",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "purchase_order_tax",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AlterColumn<string>(
            name: "purchase_order_notes",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(500)",
            oldMaxLength: 500,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "purchase_order_no",
            schema: "oms",
            table: "purchase_order_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(20)",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<decimal>(
            name: "purchase_order_amount",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "purchase_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1),
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "paid_by",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "expected_shipping_date",
            schema: "oms",
            table: "purchase_order_headers",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1),
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "delivery_charge",
            schema: "oms",
            table: "purchase_order_headers",
            type: "numeric",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AddColumn<string>(
            name: "location_name",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "shipping_address_country",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "shipping_address_county",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "shipping_address_postal_code",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "shipping_address_town_city",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "supplier_contact",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "supplier_email",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "supplier_phone",
            schema: "oms",
            table: "purchase_order_headers",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
