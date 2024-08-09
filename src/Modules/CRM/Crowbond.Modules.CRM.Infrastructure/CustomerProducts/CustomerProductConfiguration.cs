using Crowbond.Modules.CRM.Domain.Categories;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;
internal sealed class CustomerProductConfiguration : IEntityTypeConfiguration<CustomerProduct>
{
    public void Configure(EntityTypeBuilder<CustomerProduct> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.CustomerId, c.ProductId }).IsUnique();
        builder.Property(c => c.ProductSku).IsRequired().HasMaxLength(20);
        builder.Property(c => c.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.CategoryId).IsRequired();

        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId);

        builder.HasOne<Category>().WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
