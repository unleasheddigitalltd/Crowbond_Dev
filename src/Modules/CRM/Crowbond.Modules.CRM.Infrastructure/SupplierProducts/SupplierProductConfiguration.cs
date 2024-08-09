using Crowbond.Modules.CRM.Domain.Categories;
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

        builder.Property(sp => sp.ProductSku).IsRequired().HasMaxLength(20);
        builder.Property(sp => sp.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(sp => sp.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(sp => sp.CategoryId).IsRequired();
        builder.Property(sp => sp.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(sp => sp.Comments).HasMaxLength(255);

        builder.HasOne<Supplier>().WithMany().HasForeignKey(sp => sp.SupplierId);
        builder.HasOne<Category>().WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.NoAction);
    }
}
