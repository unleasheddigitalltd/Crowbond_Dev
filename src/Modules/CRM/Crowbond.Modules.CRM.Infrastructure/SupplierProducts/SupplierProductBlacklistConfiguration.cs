using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierProducts;

internal sealed class SupplierProductBlacklistConfiguration : IEntityTypeConfiguration<SupplierProductBlacklist>
{
    public void Configure(EntityTypeBuilder<SupplierProductBlacklist> builder)
    {
        builder.ToTable("supplier_product_blacklist");
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Comments).HasMaxLength(255);

        builder.HasOne<Supplier>().WithMany().HasForeignKey(sp => sp.SupplierId);
        builder.HasOne<Product>().WithMany().HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}
