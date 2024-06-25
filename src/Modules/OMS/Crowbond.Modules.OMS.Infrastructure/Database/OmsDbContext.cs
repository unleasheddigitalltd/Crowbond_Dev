using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Database;

public sealed class OmsDbContext(DbContextOptions<OmsDbContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.OMS);
    }
}
