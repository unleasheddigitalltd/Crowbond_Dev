using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Users_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_warehouse_operators_email",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropIndex(
            name: "ix_warehouse_operators_username",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropColumn(
            name: "email",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropColumn(
            name: "first_name",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropColumn(
            name: "last_name",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropColumn(
            name: "mobile",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.DropColumn(
            name: "username",
            schema: "wms",
            table: "warehouse_operators");

        migrationBuilder.CreateTable(
            name: "users",
            schema: "wms",
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
            schema: "wms",
            table: "users",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_mobile",
            schema: "wms",
            table: "users",
            column: "mobile",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_username",
            schema: "wms",
            table: "users",
            column: "username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users",
            schema: "wms");

        migrationBuilder.AddColumn<string>(
            name: "email",
            schema: "wms",
            table: "warehouse_operators",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "first_name",
            schema: "wms",
            table: "warehouse_operators",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "last_name",
            schema: "wms",
            table: "warehouse_operators",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "mobile",
            schema: "wms",
            table: "warehouse_operators",
            type: "character varying(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "username",
            schema: "wms",
            table: "warehouse_operators",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

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
    }
}
