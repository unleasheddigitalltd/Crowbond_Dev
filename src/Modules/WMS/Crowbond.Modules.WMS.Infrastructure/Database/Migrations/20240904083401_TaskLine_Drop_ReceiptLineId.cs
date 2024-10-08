﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.WMS.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class TaskLine_Drop_ReceiptLineId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "receipt_line_id",
            schema: "wms",
            table: "task_assignment_lines",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);
    }
}
