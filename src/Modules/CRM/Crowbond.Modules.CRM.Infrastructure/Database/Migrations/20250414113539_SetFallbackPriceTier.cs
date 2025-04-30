using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crowbond.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SetFallbackPriceTier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE crm.price_tiers SET is_fallback_tier = true WHERE id = 'b838c1ad-caa5-4c3b-8b2e-34fce348b69e';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE crm.price_tiers SET is_fallback_tier = false WHERE id = 'b838c1ad-caa5-4c3b-8b2e-34fce348b69e';");
        }
    }
}
