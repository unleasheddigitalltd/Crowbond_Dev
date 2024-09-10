using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;
internal sealed class CustomerProductConfiguration : IEntityTypeConfiguration<CustomerProduct>
{
    public void Configure(EntityTypeBuilder<CustomerProduct> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FixedPrice).HasPrecision(10, 2);
        builder.Property(c => c.FixedDiscount).HasPrecision(5, 2);
        builder.Property(c => c.Comments).HasMaxLength(255);

        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId);
        builder.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId);
    }
}
