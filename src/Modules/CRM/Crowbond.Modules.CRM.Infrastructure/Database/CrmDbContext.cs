using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.CRM.Infrastructure.Suppliers;
using Crowbond.Modules.CRM.Infrastructure.Customers;
using Crowbond.Modules.CRM.Domain.Sequences;
using Crowbond.Modules.CRM.Infrastructure.Sequences;

namespace Crowbond.Modules.CRM.Infrastructure.Database;
public sealed class CrmDbContext(DbContextOptions<CrmDbContext> options) : DbContext(options), IUnitOfWork
{

    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<Supplier> Suppliers { get; set; }
    internal DbSet<Sequence> Sequences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.CRM);
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());
    }
}
