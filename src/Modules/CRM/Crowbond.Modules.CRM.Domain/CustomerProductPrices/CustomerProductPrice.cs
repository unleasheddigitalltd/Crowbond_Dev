using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProductPrices;

public sealed class CustomerProductPrice : Entity
{
    private CustomerProductPrice()
    {
        
    }

    public Guid Id { get; private set; }

    public Guid CustomerProductId { get; private set; }

    public decimal? FixedPrice { get; private set; }

    public decimal? FixedDiscount { get; private set; }

    public string? Comment { get; private set; }

    public DateOnly EffectiveDate { get; private set; }

    public DateOnly? ExpiryDate { get; private set; }

    public static CustomerProductPrice Create(
        Guid customerProductId,
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comment,
        DateOnly effectiveDate,
        DateOnly? expiryDate)
    {
        var customerProductPrice = new CustomerProductPrice
        {
            Id = Guid.NewGuid(),
            CustomerProductId = customerProductId,
            FixedPrice = fixedPrice,
            FixedDiscount = fixedDiscount,
            Comment = comment,
            EffectiveDate = effectiveDate,
            ExpiryDate = expiryDate
        };

        return customerProductPrice;
    }

    public void Update(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comment,
        DateOnly effectiveDate,
        DateOnly? expiryDate)
    {
        FixedPrice = fixedPrice;
        FixedDiscount = fixedDiscount;
        Comment = comment;
        EffectiveDate = effectiveDate;
        ExpiryDate = expiryDate;
    }
}
