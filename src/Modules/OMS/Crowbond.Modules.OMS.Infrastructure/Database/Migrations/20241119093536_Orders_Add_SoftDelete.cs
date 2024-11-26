// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Orders_Add_SoftDelete : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "deleted_by",
            schema: "oms",
            table: "order_headers",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "deleted_on_utc",
            schema: "oms",
            table: "order_headers",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "is_deleted",
            schema: "oms",
            table: "order_headers",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "deleted_by",
            schema: "oms",
            table: "order_headers");

        migrationBuilder.DropColumn(
            name: "deleted_on_utc",
            schema: "oms",
            table: "order_headers");

        migrationBuilder.DropColumn(
            name: "is_deleted",
            schema: "oms",
            table: "order_headers");
    }
}
