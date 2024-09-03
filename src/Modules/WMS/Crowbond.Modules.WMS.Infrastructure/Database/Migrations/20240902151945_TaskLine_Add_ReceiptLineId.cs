﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class TaskLine_Add_ReceiptLineId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.AddColumn<Guid>(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "use_by_date",
            schema: "wms",
            table: "stocks",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "sell_by_date",
            schema: "wms",
            table: "stocks",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "reason_id",
            schema: "wms",
            table: "stock_transactions",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.InsertData(
            schema: "wms",
            table: "action_types",
            column: "name",
            value: "PutAway");

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions",
            column: "reason_id",
            principalSchema: "wms",
            principalTable: "stock_transaction_reasons",
            principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DeleteData(
            schema: "wms",
            table: "action_types",
            keyColumn: "name",
            keyValue: "PutAway");

        migrationBuilder.DropColumn(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.AlterColumn<DateTime>(
            name: "use_by_date",
            schema: "wms",
            table: "stocks",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "sell_by_date",
            schema: "wms",
            table: "stocks",
            type: "timestamp with time zone",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "reason_id",
            schema: "wms",
            table: "stock_transactions",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions",
            column: "reason_id",
            principalSchema: "wms",
            principalTable: "stock_transaction_reasons",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}