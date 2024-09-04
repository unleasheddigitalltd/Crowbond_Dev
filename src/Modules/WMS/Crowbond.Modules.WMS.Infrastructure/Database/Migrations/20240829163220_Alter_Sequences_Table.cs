using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Alter_Sequences_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_task_assignment_line_task_assignment_lin",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_task_header_task_header_id",
            schema: "wms",
            table: "task_assignment");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_line_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_line");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_line_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_line");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_line_products_product_id",
            schema: "wms",
            table: "task_assignment_line");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_line_task_assignment_task_assignment_id",
            schema: "wms",
            table: "task_assignment_line");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_status_history_task_assignment_task_assignm",
            schema: "wms",
            table: "task_assignment_status_history");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_header",
            schema: "wms",
            table: "task_header");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignment_status_history",
            schema: "wms",
            table: "task_assignment_status_history");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignment_line",
            schema: "wms",
            table: "task_assignment_line");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignment",
            schema: "wms",
            table: "task_assignment");

        migrationBuilder.RenameTable(
            name: "task_header",
            schema: "wms",
            newName: "task_headers",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignment_status_history",
            schema: "wms",
            newName: "task_assignment_status_histories",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignment_line",
            schema: "wms",
            newName: "task_assignment_lines",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignment",
            schema: "wms",
            newName: "task_assignments",
            newSchema: "wms");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_status_history_task_assignment_id",
            schema: "wms",
            table: "task_assignment_status_histories",
            newName: "ix_task_assignment_status_histories_task_assignment_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_line_to_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "ix_task_assignment_lines_to_location_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_line_task_assignment_id",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "ix_task_assignment_lines_task_assignment_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_line_product_id",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "ix_task_assignment_lines_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_line_from_location_id",
            schema: "wms",
            table: "task_assignment_lines",
            newName: "ix_task_assignment_lines_from_location_id");

        migrationBuilder.RenameColumn(
            name: "assigned_user_id",
            schema: "wms",
            table: "task_assignments",
            newName: "assigned_operator_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_task_header_id",
            schema: "wms",
            table: "task_assignments",
            newName: "ix_task_assignments_task_header_id");

        migrationBuilder.AddColumn<string>(
            name: "prefix",
            schema: "wms",
            table: "sequences",
            type: "character varying(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<int>(
            name: "location_type",
            schema: "wms",
            table: "locations",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_headers",
            schema: "wms",
            table: "task_headers",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignment_status_histories",
            schema: "wms",
            table: "task_assignment_status_histories",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignment_lines",
            schema: "wms",
            table: "task_assignment_lines",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignments",
            schema: "wms",
            table: "task_assignments",
            column: "id");

        migrationBuilder.CreateTable(
            name: "warehouse_operators",
            schema: "wms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_warehouse_operators", x => x.id);
            });

        migrationBuilder.UpdateData(
            schema: "wms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 0,
            column: "prefix",
            value: "RCP");

        migrationBuilder.UpdateData(
            schema: "wms",
            table: "sequences",
            keyColumn: "context",
            keyValue: 1,
            column: "prefix",
            value: "TSK");

        migrationBuilder.CreateIndex(
            name: "ix_task_headers_entity_id",
            schema: "wms",
            table: "task_headers",
            column: "entity_id");

        migrationBuilder.CreateIndex(
            name: "ix_task_assignments_assigned_operator_id",
            schema: "wms",
            table: "task_assignments",
            column: "assigned_operator_id");

        migrationBuilder.CreateIndex(
            name: "ix_warehouse_operators_email",
            schema: "wms",
            table: "warehouse_operators",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_warehouse_operators_username",
            schema: "wms",
            table: "warehouse_operators",
            column: "username",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_task_assignment_lines_task_assignment_li",
            schema: "wms",
            table: "stock_transactions",
            column: "task_assignment_line_id",
            principalSchema: "wms",
            principalTable: "task_assignment_lines",
            principalColumn: "id");

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
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_products_product_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "product_id",
            principalSchema: "wms",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_lines_task_assignments_task_assignment_id",
            schema: "wms",
            table: "task_assignment_lines",
            column: "task_assignment_id",
            principalSchema: "wms",
            principalTable: "task_assignments",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_status_histories_task_assignments_task_assi",
            schema: "wms",
            table: "task_assignment_status_histories",
            column: "task_assignment_id",
            principalSchema: "wms",
            principalTable: "task_assignments",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignments_task_headers_task_header_id",
            schema: "wms",
            table: "task_assignments",
            column: "task_header_id",
            principalSchema: "wms",
            principalTable: "task_headers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignments_warehouse_operators_assigned_operator_id",
            schema: "wms",
            table: "task_assignments",
            column: "assigned_operator_id",
            principalSchema: "wms",
            principalTable: "warehouse_operators",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_headers_receipt_headers_entity_id",
            schema: "wms",
            table: "task_headers",
            column: "entity_id",
            principalSchema: "wms",
            principalTable: "receipt_headers",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_stock_transactions_task_assignment_lines_task_assignment_li",
            schema: "wms",
            table: "stock_transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_products_product_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_lines_task_assignments_task_assignment_id",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignment_status_histories_task_assignments_task_assi",
            schema: "wms",
            table: "task_assignment_status_histories");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignments_task_headers_task_header_id",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropForeignKey(
            name: "fk_task_assignments_warehouse_operators_assigned_operator_id",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropForeignKey(
            name: "fk_task_headers_receipt_headers_entity_id",
            schema: "wms",
            table: "task_headers");

        migrationBuilder.DropTable(
            name: "warehouse_operators",
            schema: "wms");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_headers",
            schema: "wms",
            table: "task_headers");

        migrationBuilder.DropIndex(
            name: "ix_task_headers_entity_id",
            schema: "wms",
            table: "task_headers");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignments",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropIndex(
            name: "ix_task_assignments_assigned_operator_id",
            schema: "wms",
            table: "task_assignments");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignment_status_histories",
            schema: "wms",
            table: "task_assignment_status_histories");

        migrationBuilder.DropPrimaryKey(
            name: "pk_task_assignment_lines",
            schema: "wms",
            table: "task_assignment_lines");

        migrationBuilder.DropColumn(
            name: "prefix",
            schema: "wms",
            table: "sequences");

        migrationBuilder.RenameTable(
            name: "task_headers",
            schema: "wms",
            newName: "task_header",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignments",
            schema: "wms",
            newName: "task_assignment",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignment_status_histories",
            schema: "wms",
            newName: "task_assignment_status_history",
            newSchema: "wms");

        migrationBuilder.RenameTable(
            name: "task_assignment_lines",
            schema: "wms",
            newName: "task_assignment_line",
            newSchema: "wms");

        migrationBuilder.RenameColumn(
            name: "assigned_operator_id",
            schema: "wms",
            table: "task_assignment",
            newName: "assigned_user_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignments_task_header_id",
            schema: "wms",
            table: "task_assignment",
            newName: "ix_task_assignment_task_header_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_status_histories_task_assignment_id",
            schema: "wms",
            table: "task_assignment_status_history",
            newName: "ix_task_assignment_status_history_task_assignment_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_lines_to_location_id",
            schema: "wms",
            table: "task_assignment_line",
            newName: "ix_task_assignment_line_to_location_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_lines_task_assignment_id",
            schema: "wms",
            table: "task_assignment_line",
            newName: "ix_task_assignment_line_task_assignment_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_lines_product_id",
            schema: "wms",
            table: "task_assignment_line",
            newName: "ix_task_assignment_line_product_id");

        migrationBuilder.RenameIndex(
            name: "ix_task_assignment_lines_from_location_id",
            schema: "wms",
            table: "task_assignment_line",
            newName: "ix_task_assignment_line_from_location_id");

        migrationBuilder.AlterColumn<int>(
            name: "location_type",
            schema: "wms",
            table: "locations",
            type: "integer",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_header",
            schema: "wms",
            table: "task_header",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignment",
            schema: "wms",
            table: "task_assignment",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignment_status_history",
            schema: "wms",
            table: "task_assignment_status_history",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_task_assignment_line",
            schema: "wms",
            table: "task_assignment_line",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_stock_transactions_task_assignment_line_task_assignment_lin",
            schema: "wms",
            table: "stock_transactions",
            column: "task_assignment_line_id",
            principalSchema: "wms",
            principalTable: "task_assignment_line",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_task_header_task_header_id",
            schema: "wms",
            table: "task_assignment",
            column: "task_header_id",
            principalSchema: "wms",
            principalTable: "task_header",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_line_locations_from_location_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "from_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_line_locations_to_location_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "to_location_id",
            principalSchema: "wms",
            principalTable: "locations",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_line_products_product_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "product_id",
            principalSchema: "wms",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_line_task_assignment_task_assignment_id",
            schema: "wms",
            table: "task_assignment_line",
            column: "task_assignment_id",
            principalSchema: "wms",
            principalTable: "task_assignment",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_task_assignment_status_history_task_assignment_task_assignm",
            schema: "wms",
            table: "task_assignment_status_history",
            column: "task_assignment_id",
            principalSchema: "wms",
            principalTable: "task_assignment",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
