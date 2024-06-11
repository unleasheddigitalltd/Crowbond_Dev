using Crowbond.Modules.Warehouse.Domain.Locations;
using Crowbond.Modules.Warehouse.Domain.Receipts;
using Crowbond.Modules.Warehouse.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Warehouse.Infrastructure.Stocks;

internal sealed class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.OriginalQty).IsRequired().HasPrecision(10, 2);
        builder.Property(s => s.CurrentQty).IsRequired().HasPrecision(10, 2);
        builder.Property(s => s.BatchNumber).IsRequired(false).HasMaxLength(255);
        builder.Property(s => s.SellByDate).IsRequired(false);
        builder.Property(s => s.UseByDate).IsRequired(false);
        builder.Property(s => s.Note).IsRequired(false).HasMaxLength(255);

        builder.HasOne<Location>().WithMany().HasForeignKey(s => s.LocationId);
        builder.HasOne<ReceiptLine>().WithMany().HasForeignKey(s => s.ReceiptId);
    }
}
