// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Alter_TaskLineQty_NotNull : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "missed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "completed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2,
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "missed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AlterColumn<decimal>(
            name: "completed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);
    }
}
