// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class ActionType_Insert_BlindReceive : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            schema: "wms",
            table: "action_types",
            column: "name",
            value: "BlinedReceive");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "wms",
            table: "action_types",
            keyColumn: "name",
            keyValue: "BlinedReceive");
    }
}
