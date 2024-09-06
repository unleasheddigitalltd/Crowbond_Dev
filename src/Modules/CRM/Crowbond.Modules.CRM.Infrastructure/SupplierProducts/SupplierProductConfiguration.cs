using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierProducts;

internal sealed class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
{
    public void Configure(EntityTypeBuilder<SupplierProduct> builder)
    {
        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(sp => sp.Comments).HasMaxLength(255);
        builder.Property(sp => sp.LastModifiedBy).IsRequired(false);
        builder.Property(sp => sp.LastModifiedOnUtc).IsRequired(false);

        builder.HasOne<Supplier>().WithMany().HasForeignKey(sp => sp.SupplierId);
        builder.HasOne<Product>().WithMany().HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}
