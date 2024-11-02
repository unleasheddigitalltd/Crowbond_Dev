// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Compliances_Tables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "temperature",
            schema: "oms",
            table: "route_trip_logs");

        migrationBuilder.DropColumn(
            name: "vehicle_id",
            schema: "oms",
            table: "route_trip_logs");

        migrationBuilder.AddColumn<int>(
            name: "length",
            schema: "oms",
            table: "sequences",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "compliance_headers",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                route_trip_log_id = table.Column<Guid>(type: "uuid", nullable: false),
                vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                form_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                form_date = table.Column<DateOnly>(type: "date", nullable: false),
                temperature = table.Column<decimal>(type: "numeric(2,2)", precision: 2, scale: 2, nullable: true),
                is_confirmed = table.Column<bool>(type: "boolean", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_compliance_headers", x => x.id);
                table.ForeignKey(
                    name: "fk_compliance_headers_route_trip_logs_route_trip_log_id",
                    column: x => x.route_trip_log_id,
                    principalSchema: "oms",
                    principalTable: "route_trip_logs",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_compliance_headers_vehicles_vehicle_id",
                    column: x => x.vehicle_id,
                    principalSchema: "oms",
                    principalTable: "vehicles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "compliance_questions",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_compliance_questions", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "compliance_lines",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                compliance_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                compliance_question_id = table.Column<Guid>(type: "uuid", nullable: false),
                description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                response = table.Column<bool>(type: "boolean", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_compliance_lines", x => x.id);
                table.ForeignKey(
                    name: "fk_compliance_lines_compliance_headers_compliance_header_id",
                    column: x => x.compliance_header_id,
                    principalSchema: "oms",
                    principalTable: "compliance_headers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_compliance_lines_compliance_questions_compliance_question_id",
                    column: x => x.compliance_question_id,
                    principalSchema: "oms",
                    principalTable: "compliance_questions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 0,
            column: "length",
            value: 5);

        migrationBuilder.UpdateData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 1,
            column: "length",
            value: 5);

        migrationBuilder.UpdateData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 2,
            column: "length",
            value: 5);

        migrationBuilder.InsertData(
            schema: "oms",
            table: "sequences",
            columns: [ "context", "last_number", "length", "prefix" ],
            values: new object[] { 3, 10001, 5, "CMP" });

        migrationBuilder.CreateIndex(
            name: "ix_compliance_headers_route_trip_log_id",
            schema: "oms",
            table: "compliance_headers",
            column: "route_trip_log_id");

        migrationBuilder.CreateIndex(
            name: "ix_compliance_headers_vehicle_id",
            schema: "oms",
            table: "compliance_headers",
            column: "vehicle_id");

        migrationBuilder.CreateIndex(
            name: "ix_compliance_lines_compliance_header_id",
            schema: "oms",
            table: "compliance_lines",
            column: "compliance_header_id");

        migrationBuilder.CreateIndex(
            name: "ix_compliance_lines_compliance_question_id",
            schema: "oms",
            table: "compliance_lines",
            column: "compliance_question_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "compliance_lines",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "compliance_headers",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "compliance_questions",
            schema: "oms");

        migrationBuilder.DeleteData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 3);

        migrationBuilder.DropColumn(
            name: "length",
            schema: "oms",
            table: "sequences");

        migrationBuilder.AddColumn<decimal>(
            name: "temperature",
            schema: "oms",
            table: "route_trip_logs",
            type: "numeric(2,2)",
            precision: 2,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "vehicle_id",
            schema: "oms",
            table: "route_trip_logs",
            type: "uuid",
            nullable: true);

        migrationBuilder.UpdateData(
            schema: "oms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 2,
            column: "last_number",
            value: 10001);
    }
}
