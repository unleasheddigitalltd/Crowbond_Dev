using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.CustomerSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerSettings;
public class CustomerSettingConfiguration : IEntityTypeConfiguration<CustomerSetting>
{
    public void Configure(EntityTypeBuilder<CustomerSetting> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CustomerLogo).HasMaxLength(100);

        builder.HasOne<Customer>()
                   .WithOne(c => c.CustomerSetting)
                   .HasForeignKey<CustomerSetting>(cs => cs.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
    }
}
