using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_CustomerProduct_JobFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "error_message",
            schema: "crm",
            table: "customer_products",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "processed_on_utc",
            schema: "crm",
            table: "customer_products",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "error_message",
            schema: "crm",
            table: "customer_products");

        migrationBuilder.DropColumn(
            name: "processed_on_utc",
            schema: "crm",
            table: "customer_products");
    }
}
