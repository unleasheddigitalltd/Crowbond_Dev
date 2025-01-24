// <auto-generate />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CustomerOutletRoute_Alter_WeekDay_Type : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "days_of_week",
            schema: "crm",
            table: "customer_outlet_routes");

        migrationBuilder.AddColumn<int>(
            name: "weekday",
            schema: "crm",
            table: "customer_outlet_routes",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "ix_customer_outlet_routes_customer_outlet_id",
            schema: "crm",
            table: "customer_outlet_routes",
            column: "customer_outlet_id");

        migrationBuilder.AddForeignKey(
            name: "fk_customer_outlet_routes_customer_outlets_customer_outlet_id",
            schema: "crm",
            table: "customer_outlet_routes",
            column: "customer_outlet_id",
            principalSchema: "crm",
            principalTable: "customer_outlets",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_customer_outlet_routes_customer_outlets_customer_outlet_id",
            schema: "crm",
            table: "customer_outlet_routes");

        migrationBuilder.DropIndex(
            name: "ix_customer_outlet_routes_customer_outlet_id",
            schema: "crm",
            table: "customer_outlet_routes");

        migrationBuilder.DropColumn(
            name: "weekday",
            schema: "crm",
            table: "customer_outlet_routes");

        migrationBuilder.AddColumn<string>(
            name: "days_of_week",
            schema: "crm",
            table: "customer_outlet_routes",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
