// <auto_generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Users_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_drivers_email",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropIndex(
            name: "ix_drivers_username",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "email",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "first_name",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "last_name",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "mobile",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "username",
            schema: "oms",
            table: "drivers");

        migrationBuilder.DropColumn(
            name: "vehicle_regn",
            schema: "oms",
            table: "drivers");

        migrationBuilder.CreateTable(
            name: "users",
            schema: "oms",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                username = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                is_active = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "oms",
            table: "users",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_mobile",
            schema: "oms",
            table: "users",
            column: "mobile",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_username",
            schema: "oms",
            table: "users",
            column: "username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users",
            schema: "oms");

        migrationBuilder.AddColumn<string>(
            name: "email",
            schema: "oms",
            table: "drivers",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "first_name",
            schema: "oms",
            table: "drivers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "last_name",
            schema: "oms",
            table: "drivers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "mobile",
            schema: "oms",
            table: "drivers",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "username",
            schema: "oms",
            table: "drivers",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "vehicle_regn",
            schema: "oms",
            table: "drivers",
            type: "character varying(10)",
            maxLength: 10,
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_drivers_email",
            schema: "oms",
            table: "drivers",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_drivers_username",
            schema: "oms",
            table: "drivers",
            column: "username",
            unique: true);
    }
}
