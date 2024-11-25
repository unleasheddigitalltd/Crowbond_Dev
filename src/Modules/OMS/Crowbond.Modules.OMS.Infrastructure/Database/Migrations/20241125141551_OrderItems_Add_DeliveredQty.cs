// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class OrderItems_Add_DeliveredQty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "qty",
            schema: "oms",
            table: "order_lines",
            newName: "ordered_qty");

        migrationBuilder.AddColumn<decimal>(
            name: "actual_qty",
            schema: "oms",
            table: "order_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "deduction_line_total",
            schema: "oms",
            table: "order_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "deduction_sub_total",
            schema: "oms",
            table: "order_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "deduction_tax",
            schema: "oms",
            table: "order_lines",
            type: "numeric(5,2)",
            precision: 5,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "delivered_qty",
            schema: "oms",
            table: "order_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "delivery_comments",
            schema: "oms",
            table: "order_lines",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "reject_reason_id",
            schema: "oms",
            table: "order_lines",
            type: "uuid",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "actual_qty",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "deduction_line_total",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "deduction_sub_total",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "deduction_tax",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "delivered_qty",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "delivery_comments",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "reject_reason_id",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.RenameColumn(
            name: "ordered_qty",
            schema: "oms",
            table: "order_lines",
            newName: "qty");
    }
}
