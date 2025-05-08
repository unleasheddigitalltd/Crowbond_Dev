using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Locations_NetworkAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "network_address",
                schema: "wms",
                table: "locations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "printer_name",
                schema: "wms",
                table: "locations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "network_address",
                schema: "wms",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "printer_name",
                schema: "wms",
                table: "locations");
        }
    }
}
