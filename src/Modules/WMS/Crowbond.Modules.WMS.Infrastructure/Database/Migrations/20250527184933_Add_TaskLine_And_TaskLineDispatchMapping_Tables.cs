using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_TaskLine_And_TaskLineDispatchMapping_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "wms",
                table: "task_headers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "route_trip_id",
                schema: "wms",
                table: "task_headers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "scheduled_delivery_date",
                schema: "wms",
                table: "task_headers",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "task_lines",
                schema: "wms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    completed_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_lines", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_lines_task_headers_task_header_id",
                        column: x => x.task_header_id,
                        principalSchema: "wms",
                        principalTable: "task_headers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_line_dispatch_mappings",
                schema: "wms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dispatch_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                    allocated_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    completed_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_line_dispatch_mappings", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_line_dispatch_mappings_task_lines_task_line_id",
                        column: x => x.task_line_id,
                        principalSchema: "wms",
                        principalTable: "task_lines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_task_line_dispatch_mappings_dispatch_line_id",
                schema: "wms",
                table: "task_line_dispatch_mappings",
                column: "dispatch_line_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_line_dispatch_mappings_task_line_id",
                schema: "wms",
                table: "task_line_dispatch_mappings",
                column: "task_line_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_lines_task_header_id",
                schema: "wms",
                table: "task_lines",
                column: "task_header_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_line_dispatch_mappings",
                schema: "wms");

            migrationBuilder.DropTable(
                name: "task_lines",
                schema: "wms");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "wms",
                table: "task_headers");

            migrationBuilder.DropColumn(
                name: "route_trip_id",
                schema: "wms",
                table: "task_headers");

            migrationBuilder.DropColumn(
                name: "scheduled_delivery_date",
                schema: "wms",
                table: "task_headers");
        }
    }
}
