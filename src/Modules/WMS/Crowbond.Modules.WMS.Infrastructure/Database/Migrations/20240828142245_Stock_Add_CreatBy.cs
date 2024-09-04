using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Stock_Add_CreatBy : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "task_number",
            schema: "wms",
            table: "task_header",
            newName: "task_no");

        migrationBuilder.AddColumn<Guid>(
            name: "created_by",
            schema: "wms",
            table: "stocks",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "created_date",
            schema: "wms",
            table: "stocks",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "wms",
            table: "stocks",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_date",
            schema: "wms",
            table: "stocks",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "receipt_no",
            schema: "wms",
            table: "receipt_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "created_by",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropColumn(
            name: "created_date",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropColumn(
            name: "last_modified_date",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropColumn(
            name: "receipt_no",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.RenameColumn(
            name: "task_no",
            schema: "wms",
            table: "task_header",
            newName: "task_number");
    }
}
