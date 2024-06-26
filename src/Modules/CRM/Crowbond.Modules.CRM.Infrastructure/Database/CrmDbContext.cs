using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Database;
public sealed class CrmDbContext(DbContextOptions<CrmDbContext> options) : DbContext(options), IUnitOfWork
{

    internal DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.CRM);
    }
}
