// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Users_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "users",
            schema: "crm",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "crm",
            table: "users",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_mobile",
            schema: "crm",
            table: "users",
            column: "mobile",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_username",
            schema: "crm",
            table: "users",
            column: "username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users",
            schema: "crm");
    }
}
