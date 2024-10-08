﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Rename_AuditDate_Columns : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "last_modified_date",
            schema: "wms",
            table: "task_assignments",
            newName: "last_modified_on_utc");

        migrationBuilder.RenameColumn(
            name: "created_date",
            schema: "wms",
            table: "task_assignments",
            newName: "created_on_utc");

        migrationBuilder.RenameColumn(
            name: "last_modified_date",
            schema: "wms",
            table: "stocks",
            newName: "last_modified_on_utc");

        migrationBuilder.RenameColumn(
            name: "created_date",
            schema: "wms",
            table: "stocks",
            newName: "created_on_utc");

        migrationBuilder.RenameColumn(
            name: "last_modified_date",
            schema: "wms",
            table: "receipt_headers",
            newName: "last_modified_on_utc");

        migrationBuilder.RenameColumn(
            name: "created_date",
            schema: "wms",
            table: "receipt_headers",
            newName: "created_on_utc");

        migrationBuilder.AddColumn<Guid>(
            name: "deleted_by",
            schema: "wms",
            table: "settings",
            type: "uuid",
            nullable: true);

        migrationBuilder.UpdateData(
            schema: "wms",
            table: "settings",
            keyColumn: "id",
            keyValue: new Guid("037b725f-2110-40f8-a1b3-06ca5722cb83"),
            column: "deleted_by",
            value: null);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "deleted_by",
            schema: "wms",
            table: "settings");

        migrationBuilder.RenameColumn(
            name: "last_modified_on_utc",
            schema: "wms",
            table: "task_assignments",
            newName: "last_modified_date");

        migrationBuilder.RenameColumn(
            name: "created_on_utc",
            schema: "wms",
            table: "task_assignments",
            newName: "created_date");

        migrationBuilder.RenameColumn(
            name: "last_modified_on_utc",
            schema: "wms",
            table: "stocks",
            newName: "last_modified_date");

        migrationBuilder.RenameColumn(
            name: "created_on_utc",
            schema: "wms",
            table: "stocks",
            newName: "created_date");

        migrationBuilder.RenameColumn(
            name: "last_modified_on_utc",
            schema: "wms",
            table: "receipt_headers",
            newName: "last_modified_date");

        migrationBuilder.RenameColumn(
            name: "created_on_utc",
            schema: "wms",
            table: "receipt_headers",
            newName: "created_date");
    }
}
