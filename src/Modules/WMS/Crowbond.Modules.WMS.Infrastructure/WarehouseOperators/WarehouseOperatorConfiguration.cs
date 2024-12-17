using Crowbond.Modules.WMS.Domain.Users;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.WarehouseOperators;

internal sealed class WarehouseOperatorConfiguration : IEntityTypeConfiguration<WarehouseOperator>
{
    public void Configure(EntityTypeBuilder<WarehouseOperator> builder)
    {
        builder.HasKey(d => d.Id);
    }
}
