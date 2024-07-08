using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;

public sealed class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {

        builder.HasKey(p => p.Name);

        builder.Property(p => p.Name).HasMaxLength(100);

        builder.HasData(
            PaymentMethod.BankTransfer,
            PaymentMethod.CashOnDelivery,
            PaymentMethod.CreditCard,
            PaymentMethod.Invoice);
    }
}
