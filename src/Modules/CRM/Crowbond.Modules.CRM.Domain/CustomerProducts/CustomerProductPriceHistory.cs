using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProductPriceHistory : Entity, ITrackable
{
    private CustomerProductPriceHistory()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerProductId { get; private set; }

    public decimal? FixedPrice { get; private set; }

    public decimal? FixedDiscount { get; private set; }

    public string? Comments { get; private set; }

    public DateOnly? EffectiveDate { get; private set; }

    public DateOnly? ExpiryDate { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime ChangedAt { get; private set; }

    public Guid ChangedBy { get; set; }

    internal CustomerProductPriceHistory(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly? effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow,
        bool isActive)
    {
        Id = Guid.NewGuid();
        FixedPrice = fixedPrice;
        FixedDiscount = fixedDiscount;
        Comments = comments;
        EffectiveDate = effectiveDate;
        ExpiryDate = expiryDate;
        ChangedAt = utcNow;
        IsActive = isActive;
    }
}
