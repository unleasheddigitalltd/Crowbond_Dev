using Crowbond.Modules.Products.Application.Abstractions.Data;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.Products.Domain.Products;
using Crowbond.Modules.Products.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.Products.Domain.Categories;
using Crowbond.Modules.Products.Infrastructure.Categories;

namespace Crowbond.Modules.Products.Infrastructure.Database;

public sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Product> Products { get; set; }
    internal DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    internal DbSet<FilterType> FilterTypes { get; set; }
    internal DbSet<InventoryType> InventoryTypes { get; set; }
    internal DbSet<Category> Categories { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Products);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
        modelBuilder.ApplyConfiguration(new FilterTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
}
