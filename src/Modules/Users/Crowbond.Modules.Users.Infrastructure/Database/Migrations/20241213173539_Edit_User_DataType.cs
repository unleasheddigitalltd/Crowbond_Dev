using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Edit_User_DataType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "username",
            schema: "users",
            table: "users",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "last_name",
            schema: "users",
            table: "users",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "first_name",
            schema: "users",
            table: "users",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "email",
            schema: "users",
            table: "users",
            type: "character varying(150)",
            maxLength: 150,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(300)",
            oldMaxLength: 300);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "username",
            schema: "users",
            table: "users",
            type: "character varying(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "last_name",
            schema: "users",
            table: "users",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "first_name",
            schema: "users",
            table: "users",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "email",
            schema: "users",
            table: "users",
            type: "character varying(300)",
            maxLength: 300,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(150)",
            oldMaxLength: 150);
    }
}
