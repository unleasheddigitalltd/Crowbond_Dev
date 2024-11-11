// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Alter_Temprature_DataType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "temperature",
            schema: "oms",
            table: "compliance_headers",
            type: "numeric(4,2)",
            precision: 4,
            scale: 2,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(2,2)",
            oldPrecision: 2,
            oldScale: 2,
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "temperature",
            schema: "oms",
            table: "compliance_headers",
            type: "numeric(2,2)",
            precision: 2,
            scale: 2,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(4,2)",
            oldPrecision: 4,
            oldScale: 2,
            oldNullable: true);
    }
}
