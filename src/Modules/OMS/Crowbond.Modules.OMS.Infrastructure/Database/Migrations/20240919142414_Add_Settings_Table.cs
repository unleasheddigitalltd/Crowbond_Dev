﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Settings_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_order_lines_order_headers_order_id",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "foc",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "taxable",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.RenameColumn(
            name: "order_id",
            schema: "oms",
            table: "order_lines",
            newName: "order_header_id");

        migrationBuilder.RenameIndex(
            name: "ix_order_lines_order_id",
            schema: "oms",
            table: "order_lines",
            newName: "ix_order_lines_order_header_id");

        migrationBuilder.AddColumn<int>(
            name: "tax_rate_type",
            schema: "oms",
            table: "order_lines",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<string>(
            name: "delivery_address_line2",
            schema: "oms",
            table: "order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255);

        migrationBuilder.AddColumn<DateOnly>(
            name: "payment_due_date",
            schema: "oms",
            table: "order_headers",
            type: "date",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "payment_term",
            schema: "oms",
            table: "order_headers",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "settings",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                delivery_charge = table.Column<decimal>(type: "numeric", nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                deleted_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_settings", x => x.id);
            });

        migrationBuilder.InsertData(
            schema: "oms",
            table: "settings",
            columns: [ "id", "deleted_by", "deleted_on_utc", "delivery_charge", "is_deleted" ],
            values: new object[] { new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"), null, null, 10m, false });

        migrationBuilder.AddForeignKey(
            name: "fk_order_lines_order_headers_order_header_id",
            schema: "oms",
            table: "order_lines",
            column: "order_header_id",
            principalSchema: "oms",
            principalTable: "order_headers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_order_lines_order_headers_order_header_id",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropTable(
            name: "settings",
            schema: "oms");

        migrationBuilder.DropColumn(
            name: "tax_rate_type",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "payment_due_date",
            schema: "oms",
            table: "order_headers");

        migrationBuilder.DropColumn(
            name: "payment_term",
            schema: "oms",
            table: "order_headers");

        migrationBuilder.RenameColumn(
            name: "order_header_id",
            schema: "oms",
            table: "order_lines",
            newName: "order_id");

        migrationBuilder.RenameIndex(
            name: "ix_order_lines_order_header_id",
            schema: "oms",
            table: "order_lines",
            newName: "ix_order_lines_order_id");

        migrationBuilder.AddColumn<bool>(
            name: "foc",
            schema: "oms",
            table: "order_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "taxable",
            schema: "oms",
            table: "order_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AlterColumn<string>(
            name: "delivery_address_line2",
            schema: "oms",
            table: "order_headers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AddForeignKey(
            name: "fk_order_lines_order_headers_order_id",
            schema: "oms",
            table: "order_lines",
            column: "order_id",
            principalSchema: "oms",
            principalTable: "order_headers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
