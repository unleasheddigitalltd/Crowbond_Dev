using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations;


/// <inheritdoc />
public partial class Customer_BusinessName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "company_name",
            schema: "crm",
            table: "customers",
            newName: "business_name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "business_name",
            schema: "crm",
            table: "customers",
            newName: "company_name");
    }
}
