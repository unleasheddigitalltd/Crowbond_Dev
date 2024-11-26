// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_OrderLineReject_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "delivery_comments",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.DropColumn(
            name: "reject_reason_id",
            schema: "oms",
            table: "order_lines");

        migrationBuilder.CreateTable(
            name: "order_line_reject_reasons",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                responsibility = table.Column<int>(type: "integer", nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_line_reject_reasons", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "order_line_rejects",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                order_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                qty = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                reject_reason_id = table.Column<Guid>(type: "uuid", nullable: false),
                comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_line_rejects", x => x.id);
                table.ForeignKey(
                    name: "fk_order_line_rejects_order_line_reject_reasons_reject_reason_",
                    column: x => x.reject_reason_id,
                    principalSchema: "oms",
                    principalTable: "order_line_reject_reasons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_order_line_rejects_order_lines_order_line_id",
                    column: x => x.order_line_id,
                    principalSchema: "oms",
                    principalTable: "order_lines",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_order_line_rejects_order_line_id",
            schema: "oms",
            table: "order_line_rejects",
            column: "order_line_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_line_rejects_reject_reason_id",
            schema: "oms",
            table: "order_line_rejects",
            column: "reject_reason_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "order_line_rejects",
            schema: "oms");

        migrationBuilder.DropTable(
            name: "order_line_reject_reasons",
            schema: "oms");

        migrationBuilder.AddColumn<string>(
            name: "delivery_comments",
            schema: "oms",
            table: "order_lines",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "reject_reason_id",
            schema: "oms",
            table: "order_lines",
            type: "uuid",
            nullable: true);
    }
}
