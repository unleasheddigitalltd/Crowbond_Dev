﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Edit_PriceGroupId_DataType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Guid>(
            name: "price_group_id",
            schema: "crm",
            table: "customers",
            type: "uuid",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "price_group_id",
            schema: "crm",
            table: "customers",
            type: "integer",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "uuid");
    }
}
