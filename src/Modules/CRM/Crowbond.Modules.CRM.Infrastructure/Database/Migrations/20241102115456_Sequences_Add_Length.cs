// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Sequences_Add_Length : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "length",
            schema: "crm",
            table: "sequences",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            schema: "crm",
            table: "sequences",
            keyColumn: "context",
            keyValue: 0,
            column: "length",
            value: 5);

        migrationBuilder.UpdateData(
            schema: "crm",
            table: "sequences",
            keyColumn: "context",
            keyValue: 1,
            column: "length",
            value: 5);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "length",
            schema: "crm",
            table: "sequences");

        migrationBuilder.UpdateData(
            schema: "crm",
            table: "sequences",
            keyColumn: "context",
            keyValue: 0,
            column: "last_number",
            value: 10001);

        migrationBuilder.UpdateData(
            schema: "crm",
            table: "sequences",
            keyColumn: "context",
            keyValue: 1,
            column: "last_number",
            value: 10001);
    }
}
