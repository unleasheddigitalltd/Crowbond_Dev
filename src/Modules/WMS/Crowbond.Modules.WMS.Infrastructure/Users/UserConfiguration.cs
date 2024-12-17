using Crowbond.Modules.WMS.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).HasMaxLength(100);

        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.FirstName).HasMaxLength(100);

        builder.Property(u => u.LastName).HasMaxLength(100);

        builder.Property(u => u.Mobile).HasMaxLength(20);

        builder.HasIndex(u => u.Mobile).IsUnique();

        builder.Property(u => u.Email).HasMaxLength(150);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
