// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class TaskLine_Remove_RequestdQty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "completed_qty",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "end_date_time",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "missed_qty",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "start_date_time",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "status",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "order_no",
            schema: "wms",
            table: "dispatch_headers");

        migrationBuilder.RenameColumn(
            name: "requested_qty",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "qty");

        migrationBuilder.RenameColumn(
            name: "quantity_received",
            schema: "wms",
            table: "receipt_lines",
            newName: "stored_qty");

        migrationBuilder.RenameColumn(
            name: "qty",
            schema: "wms",
            table: "dispatch_lines",
            newName: "picked_qty");

        migrationBuilder.RenameColumn(
            name: "order_id",
            schema: "wms",
            table: "dispatch_headers",
            newName: "route_trip_id");

        migrationBuilder.AddColumn<DateTime>(
            name: "end_date_time",
            schema: "wms",
            table: "task_assignments",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "start_date_time",
            schema: "wms",
            table: "task_assignments",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "to_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "from_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "dispatch_line_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "is_stored",
            schema: "wms",
            table: "receipt_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<decimal>(
            name: "missed_qty",
            schema: "wms",
            table: "receipt_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<decimal>(
            name: "received_qty",
            schema: "wms",
            table: "receipt_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<Guid>(
            name: "created_by",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "created_on_utc",
            schema: "wms",
            table: "receipt_headers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "location_id",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "customer_business_name",
            schema: "wms",
            table: "dispatch_lines",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<bool>(
            name: "is_picked",
            schema: "wms",
            table: "dispatch_lines",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "order_id",
            schema: "wms",
            table: "dispatch_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<Guid>(
            name: "order_line_id",
            schema: "wms",
            table: "dispatch_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<string>(
            name: "order_no",
            schema: "wms",
            table: "dispatch_lines",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<decimal>(
            name: "ordered_qty",
            schema: "wms",
            table: "dispatch_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<Guid>(
            name: "created_by",
            schema: "wms",
            table: "dispatch_headers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "created_on_utc",
            schema: "wms",
            table: "dispatch_headers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "route_name",
            schema: "wms",
            table: "dispatch_headers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateOnly>(
            name: "route_trip_date",
            schema: "wms",
            table: "dispatch_headers",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "from_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "to_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "end_date_time",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropColumn(
            name: "start_date_time",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropColumn(
            name: "dispatch_line_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "is_stored",
            schema: "wms",
            table: "receipt_lines");

        migrationBuilder.DropColumn(
            name: "missed_qty",
            schema: "wms",
            table: "receipt_lines");

        migrationBuilder.DropColumn(
            name: "received_qty",
            schema: "wms",
            table: "receipt_lines");

        migrationBuilder.DropColumn(
            name: "created_by",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "created_on_utc",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "location_id",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "customer_business_name",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "is_picked",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "order_id",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "order_line_id",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "order_no",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "ordered_qty",
            schema: "wms",
            table: "dispatch_lines");

        migrationBuilder.DropColumn(
            name: "created_by",
            schema: "wms",
            table: "dispatch_headers");

        migrationBuilder.DropColumn(
            name: "created_on_utc",
            schema: "wms",
            table: "dispatch_headers");

        migrationBuilder.DropColumn(
            name: "route_name",
            schema: "wms",
            table: "dispatch_headers");

        migrationBuilder.DropColumn(
            name: "route_trip_date",
            schema: "wms",
            table: "dispatch_headers");

        migrationBuilder.RenameColumn(
            name: "qty",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "requested_qty");

        migrationBuilder.RenameColumn(
            name: "stored_qty",
            schema: "wms",
            table: "receipt_lines",
            newName: "quantity_received");

        migrationBuilder.RenameColumn(
            name: "picked_qty",
            schema: "wms",
            table: "dispatch_lines",
            newName: "qty");

        migrationBuilder.RenameColumn(
            name: "route_trip_id",
            schema: "wms",
            table: "dispatch_headers",
            newName: "order_id");

        migrationBuilder.AlterColumn<Guid>(
            name: "to_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AlterColumn<Guid>(
            name: "from_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AddColumn<decimal>(
            name: "completed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<DateTime>(
            name: "end_date_time",
            schema: "wms",
            table: "task_assignment_lines",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "missed_qty",
            schema: "wms",
            table: "task_assignment_lines",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<DateTime>(
            name: "start_date_time",
            schema: "wms",
            table: "task_assignment_lines",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "status",
            schema: "wms",
            table: "task_assignment_lines",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "order_no",
            schema: "wms",
            table: "dispatch_headers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "from_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "to_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id");
    }
}
