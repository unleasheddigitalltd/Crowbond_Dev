using Crowbond.Modules.Attendance.Infrastructure.Database;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Crowbond.Modules.Events.Infrastructure.Database;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Crowbond.Modules.Ticketing.Infrastructure.Database;
using Crowbond.Modules.Users.Infrastructure.Database;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<EventsDbContext>(scope);
        ApplyMigration<TicketingDbContext>(scope);
        ApplyMigration<AttendanceDbContext>(scope);
        ApplyMigration<WmsDbContext>(scope);
        ApplyMigration<CrmDbContext>(scope);
        ApplyMigration<OmsDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
