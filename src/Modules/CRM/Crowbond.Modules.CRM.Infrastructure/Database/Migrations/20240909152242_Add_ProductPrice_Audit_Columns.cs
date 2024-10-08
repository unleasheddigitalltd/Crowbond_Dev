﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_ProductPrice_Audit_Columns : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "created_by",
            schema: "crm",
            table: "product_prices",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "created_on_utc",
            schema: "crm",
            table: "product_prices",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "deleted_by",
            schema: "crm",
            table: "product_prices",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "deleted_on_utc",
            schema: "crm",
            table: "product_prices",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "is_deleted",
            schema: "crm",
            table: "product_prices",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "last_modified_by",
            schema: "crm",
            table: "product_prices",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "last_modified_on_utc",
            schema: "crm",
            table: "product_prices",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "created_by",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "created_on_utc",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "deleted_by",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "deleted_on_utc",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "is_deleted",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "last_modified_by",
            schema: "crm",
            table: "product_prices");

        migrationBuilder.DropColumn(
            name: "last_modified_on_utc",
            schema: "crm",
            table: "product_prices");
    }
}
