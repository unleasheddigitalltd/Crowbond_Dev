using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Database;

public sealed class OmsDbContext(DbContextOptions<OmsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<OrderHeader> OrderHeaders { get; set; }
    internal DbSet<OrderLine> OrderLines { get; set; }
    internal DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    internal DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.OMS);
    }
}
