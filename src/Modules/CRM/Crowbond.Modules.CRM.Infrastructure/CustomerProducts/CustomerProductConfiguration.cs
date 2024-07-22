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

        builder.HasIndex(c => new { c.CustomerId, c.ProductId }).IsUnique();

        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId);
        builder.HasOne<Product>().WithMany().HasForeignKey(c => c.ProductId);
    }
}
