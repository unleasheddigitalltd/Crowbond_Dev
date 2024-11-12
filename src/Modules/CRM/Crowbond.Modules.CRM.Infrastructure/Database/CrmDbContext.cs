using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.CRM.Infrastructure.Suppliers;
using Crowbond.Modules.CRM.Infrastructure.Customers;
using Crowbond.Modules.CRM.Domain.Sequences;
using Crowbond.Modules.CRM.Infrastructure.Sequences;
using Crowbond.Modules.CRM.Infrastructure.Routes;
using Crowbond.Modules.CRM.Domain.Routes;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.CRM.Infrastructure.Reps;
using Crowbond.Modules.CRM.Domain.Reps;
using Crowbond.Modules.CRM.Domain.Recipients;
using Crowbond.Modules.CRM.Infrastructure.Recipients;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Infrastructure.PriceTiers;
using Crowbond.Modules.CRM.Domain.ProductPrices;
using Crowbond.Modules.CRM.Infrastructure.ProductPrices;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;
using Crowbond.Modules.CRM.Infrastructure.CustomerContacts;
using Crowbond.Modules.CRM.Infrastructure.CustomerProducts;
using Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;
using Crowbond.Modules.CRM.Domain.CustomerSettings;
using Crowbond.Modules.CRM.Infrastructure.CustomerSettings;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.Infrastructure.SupplierContacts;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Infrastructure.SupplierProducts;
using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Infrastructure.Products;
using Crowbond.Common.Infrastructure.Configuration;
using Crowbond.Modules.CRM.Domain.Settings;
using Crowbond.Modules.CRM.Infrastructure.Settings;

namespace Crowbond.Modules.CRM.Infrastructure.Database;
public sealed class CrmDbContext(DbContextOptions<CrmDbContext> options) : DbContext(options), IUnitOfWork
{

    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<CustomerSetting> CustomerSettings { get; set; }
    internal DbSet<CustomerOutlet> CustomerOutlets { get; set; }
    internal DbSet<CustomerContact> CustomerContacts { get; set; }
    internal DbSet<Supplier> Suppliers { get; set; }
    internal DbSet<SupplierContact> SupplierContacts { get; set; }
    internal DbSet<Sequence> Sequences { get; set; }
    internal DbSet<Rep> Reps { get; set; }
    internal DbSet<Recipient> Recipients { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<Brand> Brands { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<ProductGroup> ProductGroups { get; set; }
    internal DbSet<PriceTier> PriceTiers { get; set; }
    internal DbSet<ProductPrice> ProductPrices { get; set; }
    internal DbSet<CustomerProduct> CustomerProducts { get; set; }
    internal DbSet<CustomerProductBlacklist> CustomerProductsBlackList { get; set; }
    internal DbSet<CustomerProductPriceHistory> CustomerProductPriceHistory { get; set; }
    internal DbSet<CustomerOutletRoute> CustomerOutletRoutes { get; set; }
    internal DbSet<Route> Routes { get; set; }
    internal DbSet<SupplierProduct> SupplierProducts { get; set; }
    internal DbSet<SupplierProductBlacklist> SupplierProductsBlacklist { get; set; }
    internal DbSet<Setting> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.CRM);

        modelBuilder.ApplySoftDeleteFilter();

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierContactConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerSettingConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerOutletConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerContactConfiguration());
        modelBuilder.ApplyConfiguration(new SequenceConfiguration());
        modelBuilder.ApplyConfiguration(new RepConfiguration());
        modelBuilder.ApplyConfiguration(new RecipientConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PriceTierConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPriceConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerProductConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerProductBlacklistConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerProductPriceHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerOutletConfiguration());
        modelBuilder.ApplyConfiguration(new RouteConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierProductConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierProductBlacklistConfiguration());
        modelBuilder.ApplyConfiguration(new SettingConfiguration());
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
