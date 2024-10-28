using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Domain.Settings;

public sealed class Setting : Entity, ISoftDeletable
{
    public static readonly Setting Initial = new(new Guid("847f725f-2110-40f8-a1b3-06ca5722cb83"), PaymentTerms.StandardTerm);

    private Setting(Guid id, PaymentTerms paymentTerms)
    {
        Id = id;
        PaymentTerms = paymentTerms;
    }

    private Setting()
    {
    }

    public Guid Id { get; private set; }

    public PaymentTerms PaymentTerms { get; private set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static Setting Create(PaymentTerms paymentTerms)
    {
        var setting = new Setting
        {
            PaymentTerms = paymentTerms
        };

        return setting;
    }

    public static DueDateCalculationBasis GetDueDateCalculationBasis(PaymentTerms paymentTerms)
    {
        return paymentTerms switch
        {
            PaymentTerms.ShortTerm => DueDateCalculationBasis.ShipDate,
            PaymentTerms.StandardTerm => DueDateCalculationBasis.EndOfInvoiceMonth,
            PaymentTerms.LongTerm => DueDateCalculationBasis.ShipDate,
            _ => throw new ArgumentOutOfRangeException(nameof(paymentTerms), paymentTerms, "Invalid payment term value")
        };
    }

    public static int GetDueDaysForInvoice(PaymentTerms paymentTerms)
    {
        return paymentTerms switch
        {
            PaymentTerms.ShortTerm => 7,
            PaymentTerms.StandardTerm => 14,
            PaymentTerms.LongTerm => 28,
            _ => throw new ArgumentOutOfRangeException(nameof(paymentTerms), paymentTerms, "Invalid payment term value")
        };
    }
}
