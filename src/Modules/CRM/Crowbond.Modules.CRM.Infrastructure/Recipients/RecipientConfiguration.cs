using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Recipients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Recipients;

internal sealed class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
{
    public void Configure(EntityTypeBuilder<Recipient> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne<CustomerContact>().WithMany().HasForeignKey(r => r.CustomerContactId);
    }
}
