// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Vehicles_table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "vehicle_regn",
            schema: "oms",
            table: "route_trip_logs");

        migrationBuilder.AddColumn<bool>(
            name: "compliance_completed",
            schema: "oms",
            table: "route_trip_logs",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "vehicle_id",
            schema: "oms",
            table: "route_trip_logs",
            type: "uuid",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "vehicles",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                vehicle_regn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_vehicles", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "vehicles",
            schema: "oms");

        migrationBuilder.DropColumn(
            name: "compliance_completed",
            schema: "oms",
            table: "route_trip_logs");

        migrationBuilder.DropColumn(
            name: "vehicle_id",
            schema: "oms",
            table: "route_trip_logs");

        migrationBuilder.AddColumn<string>(
            name: "vehicle_regn",
            schema: "oms",
            table: "route_trip_logs",
            type: "character varying(10)",
            maxLength: 10,
            nullable: false,
            defaultValue: "");
    }
}
