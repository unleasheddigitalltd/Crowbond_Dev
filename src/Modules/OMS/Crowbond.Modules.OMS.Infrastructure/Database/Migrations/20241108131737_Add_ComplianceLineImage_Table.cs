// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;


/// <inheritdoc />
public partial class Add_ComplianceLineImage_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "last_image_sequence",
            schema: "oms",
            table: "compliance_headers",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "compliance_line_images",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                compliance_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                image_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_compliance_line_images", x => x.id);
                table.ForeignKey(
                    name: "fk_compliance_line_images_compliance_lines_compliance_line_id",
                    column: x => x.compliance_line_id,
                    principalSchema: "oms",
                    principalTable: "compliance_lines",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_compliance_line_images_compliance_line_id",
            schema: "oms",
            table: "compliance_line_images",
            column: "compliance_line_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "compliance_line_images",
            schema: "oms");

        migrationBuilder.DropColumn(
            name: "last_image_sequence",
            schema: "oms",
            table: "compliance_headers");
    }
}
