using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.OMS.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Rename_CustomerOutletId_Column_To_Snake_Case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerOutletId",
                schema: "oms",
                table: "order_headers",
                newName: "customer_outlet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "customer_outlet_id",
                schema: "oms",
                table: "order_headers",
                newName: "CustomerOutletId");
        }
    }
}
