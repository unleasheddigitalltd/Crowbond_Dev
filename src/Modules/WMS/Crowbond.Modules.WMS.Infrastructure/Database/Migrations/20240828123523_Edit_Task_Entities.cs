
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Edit_Task_Entities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_locations_locations_type_location_type_name",
            schema: "wms",
            table: "locations");

        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_tasks_task_id",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_stocks_receipt_lines_receipt_id",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropTable(
            name: "locations_type",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "tasks",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "task_types",
            schema: "wms");

        migrationBuilder.DropIndex(
            name: "ix_locations_location_type_name",
            schema: "wms",
            table: "locations");

        migrationBuilder.DropColumn(
            name: "created_date",
            schema: "wms",
            table: "settings");

        migrationBuilder.DropColumn(
            name: "location_type_name",
            schema: "wms",
            table: "locations");

        migrationBuilder.RenameColumn(
            name: "receipt_id",
            schema: "wms",
            table: "stocks",
            newName: "receipt_line_id");

        migrationBuilder.RenameIndex(
            name: "ix_stocks_receipt_id",
            schema: "wms",
            table: "stocks",
            newName: "ix_stocks_receipt_line_id");

        migrationBuilder.RenameColumn(
            name: "task_id",
            schema: "wms",
            table: "stock_transactions",
            newName: "task_assignment_line_id");

        migrationBuilder.RenameIndex(
            name: "ix_stock_transactions_task_id",
            schema: "wms",
            table: "stock_transactions",
            newName: "ix_stock_transactions_task_assignment_line_id");

        migrationBuilder.RenameColumn(
            name: "is_active",
            schema: "wms",
            table: "settings",
            newName: "is_deleted");

        migrationBuilder.RenameColumn(
            name: "createtime_stamp",
            schema: "wms",
            table: "receipt_headers",
            newName: "created_date");

        migrationBuilder.AlterColumn<Guid>(
            name: "reason_id",
            schema: "wms",
            table: "stock_transactions",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "deleted_on_utc",
            schema: "wms",
            table: "settings",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "created_by",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "wms",
            table: "receipt_headers",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_date",
            schema: "wms",
            table: "receipt_headers",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "location_leyer",
            schema: "wms",
            table: "locations",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "location_type",
            schema: "wms",
            table: "locations",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "sequences",
            schema: "wms",
            columns: table => new
            {
                context = table.Column<int>(type: "integer", nullable: false),
                last_number = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_sequences", x => x.context);
            });

        migrationBuilder.CreateTable(
            name: "task_header",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                task_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                task_type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_task_header", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "task_assignment",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                task_header_id = table.Column<Guid>(type: "uuid", nullable: false),
                assigned_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                created_by = table.Column<Guid>(type: "uuid", nullable: false),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                last_modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                last_modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_task_assignment", x => x.id);
                table.ForeignKey(
                    name: "fk_task_assignment_task_header_task_header_id",
                    column: x => x.task_header_id,
                    principalSchema: "wms",
                    principalTable: "task_header",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "task_assignment_line",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                task_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                start_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                end_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                from_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                to_location_id = table.Column<Guid>(type: "uuid", nullable: true),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                requested_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                completed_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                missed_qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                status = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_task_assignment_line", x => x.id);
                table.ForeignKey(
                    name: "fk_task_assignment_line_locations_from_location_id",
                    column: x => x.from_location_id,
                    principalSchema: "wms",
                    principalTable: "locations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_task_assignment_line_locations_to_location_id",
                    column: x => x.to_location_id,
                    principalSchema: "wms",
                    principalTable: "locations",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_task_assignment_line_products_product_id",
                    column: x => x.product_id,
                    principalSchema: "wms",
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_task_assignment_line_task_assignment_task_assignment_id",
                    column: x => x.task_assignment_id,
                    principalSchema: "wms",
                    principalTable: "task_assignment",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "task_assignment_status_history",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                task_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                changed_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_task_assignment_status_history", x => x.id);
                table.ForeignKey(
                    name: "fk_task_assignment_status_history_task_assignment_task_assignm",
                    column: x => x.task_assignment_id,
                    principalSchema: "wms",
                    principalTable: "task_assignment",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "wms",
            table: "sequences",
            columns: [ "context", "last_number" ],
            values: new object[,]
            {
                { 0, 10001 },
                { 1, 10001 }
            });

        migrationBuilder.UpdateData(
            schema: "wms",
            table: "settings",
            keyColumn: "id",
            keyValue: new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"),
            columns: [ "deleted_on_utc", "is_deleted" ],
            values: new object[] { null, false });

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_task_header_id",
            schema: "wms",
            table: "task_assignment",
            column: "task_header_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_line_from_location_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "from_location_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_line_product_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_line_task_assignment_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "task_assignment_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_line_to_location_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "to_location_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignment_status_history_task_assignment_id",
            schema: "wms",
            table: "task_assignment_status_history",
            column: "task_assignment_id");

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions",
            column: "reason_id",
            principalSchema: "wms",
            principalTable: "stock_transaction_reasons",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_task_assignment_line_task_assignment_lin",
            schema: "wms",
            table: "stock_transactions",
            column: "task_assignment_line_id",
            principalSchema: "wms",
            principalTable: "task_assignment_line",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_stocks_receipt_lines_receipt_line_id",
            schema: "wms",
            table: "stocks",
            column: "receipt_line_id",
            principalSchema: "wms",
            principalTable: "receipt_lines",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_task_assignment_line_task_assignment_lin",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_stocks_receipt_lines_receipt_line_id",
            schema: "wms",
            table: "stocks");

        migrationBuilder.DropTable(
            name: "sequences",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "task_assignment_line",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "task_assignment_status_history",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "task_assignment",
            schema: "wms");

        migrationBuilder.DropTable(
            name: "task_header",
            schema: "wms");

        migrationBuilder.DropColumn(
            name: "deleted_on_utc",
            schema: "wms",
            table: "settings");

        migrationBuilder.DropColumn(
            name: "created_by",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "last_modified_date",
            schema: "wms",
            table: "receipt_headers");

        migrationBuilder.DropColumn(
            name: "Guid.Emptyter",
            schema: "wms",
            table: "locations");

        migrationBuilder.DropColumn(
            name: "location_type",
            schema: "wms",
            table: "locations");

        migrationBuilder.RenameColumn(
            name: "receipt_line_id",
            schema: "wms",
            table: "stocks",
            newName: "receipt_id");

        migrationBuilder.RenameIndex(
            name: "ix_stocks_receipt_line_id",
            schema: "wms",
            table: "stocks",
            newName: "ix_stocks_receipt_id");

        migrationBuilder.RenameColumn(
            name: "task_assignment_line_id",
            schema: "wms",
            table: "stock_transactions",
            newName: "task_id");

        migrationBuilder.RenameIndex(
            name: "ix_stock_transactions_task_assignment_line_id",
            schema: "wms",
            table: "stock_transactions",
            newName: "ix_stock_transactions_task_id");

        migrationBuilder.RenameColumn(
            name: "is_deleted",
            schema: "wms",
            table: "settings",
            newName: "is_active");

        migrationBuilder.RenameColumn(
            name: "created_date",
            schema: "wms",
            table: "receipt_headers",
            newName: "createtime_stamp");

        migrationBuilder.AlterColumn<Guid>(
            name: "reason_id",
            schema: "wms",
            table: "stock_transactions",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AddColumn<DateTime>(
            name: "created_date",
            schema: "wms",
            table: "settings",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "location_type_name",
            schema: "wms",
            table: "locations",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "locations_type",
            schema: "wms",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_locations_type", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "task_types",
            schema: "wms",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_task_types", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "tasks",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                task_type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tasks", x => x.id);
                table.ForeignKey(
                    name: "fk_tasks_task_types_task_type_name",
                    column: x => x.task_type_name,
                    principalSchema: "wms",
                    principalTable: "task_types",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "wms",
            table: "locations_type",
            column: "name",
            values: new object[]
            {
                "Area",
                "Location",
                "Site"
            });

        migrationBuilder.UpdateData(
            schema: "wms",
            table: "settings",
            keyColumn: "id",
            keyValue: new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"),
            columns: [ "created_date", "is_active" ],
            values: new object[] { new DateTime(2024, 8, 12, 20, 15, 30, 448, DateTimeKind.Utc).AddTicks(5086), true });

        migrationBuilder.CreateIndex(
            name: "ix_locations_location_type_name",
            schema: "wms",
            table: "locations",
            column: "location_type_name");

        migrationBuilder.CreateIndex(
            name: "ix_tasks_task_type_name",
            schema: "wms",
            table: "tasks",
            column: "task_type_name");

        migrationBuilder.AddForeignKey(
            name: "fk_locations_locations_type_location_type_name",
            schema: "wms",
            table: "locations",
            column: "location_type_name",
            principalSchema: "wms",
            principalTable: "locations_type",
            principalColumn: "name",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_stock_transaction_reasons_reason_id",
            schema: "wms",
            table: "stock_transactions",
            column: "reason_id",
            principalSchema: "wms",
            principalTable: "stock_transaction_reasons",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_tasks_task_id",
            schema: "wms",
            table: "stock_transactions",
            column: "task_id",
            principalSchema: "wms",
            principalTable: "tasks",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_stocks_receipt_lines_receipt_id",
            schema: "wms",
            table: "stocks",
            column: "receipt_id",
            principalSchema: "wms",
            principalTable: "receipt_lines",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
