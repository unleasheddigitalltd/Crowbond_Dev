﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Update_DeliveryTime_DataType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<TimeOnly>(
            name: "delivery_time_to",
            schema: "crm",
            table: "customer_outlets",
            type: "time without time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<TimeOnly>(
            name: "delivery_time_from",
            schema: "crm",
            table: "customer_outlets",
            type: "time without time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "delivery_time_to",
            schema: "crm",
            table: "customer_outlets",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(TimeOnly),
            oldType: "time without time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "delivery_time_from",
            schema: "crm",
            table: "customer_outlets",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(TimeOnly),
            oldType: "time without time zone");
    }
}