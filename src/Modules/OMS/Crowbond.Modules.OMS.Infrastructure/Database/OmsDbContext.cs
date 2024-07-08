using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.Orders;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Database;

public sealed class OmsDbContext(DbContextOptions<OmsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<OrderHeader> orderHeaders {  get; set; }
    internal DbSet<OrderLine> orderLines {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.OMS);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderHeaderConfiguratin());
        modelBuilder.ApplyConfiguration(new OrderLineConfiguration());
    }
}
