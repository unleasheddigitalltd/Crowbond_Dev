using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Insert_Dispatch_Sequence : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            schema: "wms",
            table: "sequences",
            columns: [ "context", "last_number", "prefix" ],
            values: new object[] { 2, 10001, "DSP" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "wms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 2);
    }
}
