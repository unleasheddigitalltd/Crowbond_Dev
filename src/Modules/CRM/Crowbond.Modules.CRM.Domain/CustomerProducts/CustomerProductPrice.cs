using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProductPrice : Entity, IAuditable
{
    private CustomerProductPrice()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerProductId { get; private set; }

    public decimal? FixedPrice { get; private set; }

    public decimal? FixedDiscount { get; private set; }

    public string? Comments { get; private set; }

    public DateOnly EffectiveDate { get; private set; }

    public DateOnly? ExpiryDate { get; private set; }

    public bool IsActive { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? ErrorMessage { get; set; }

    internal static Result<CustomerProductPrice> Create(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        if (effectiveDate < DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure<CustomerProductPrice>(CustomerProductErrors.EffectiveDateInThePast);
        }

        if (expiryDate <= DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure<CustomerProductPrice>(CustomerProductErrors.ExpiryDateInThePastOrToday);
        }

        if (expiryDate <= effectiveDate)
        {
            return Result.Failure<CustomerProductPrice>(CustomerProductErrors.ExpiryDateBeforeEffectiveDate);
        }

        var price = new CustomerProductPrice
        {
            Id = Guid.NewGuid(),
            FixedPrice = fixedPrice,
            FixedDiscount = fixedDiscount,
            Comments = comments,
            EffectiveDate = effectiveDate,
            ExpiryDate = expiryDate,
            IsActive = effectiveDate == DateOnly.FromDateTime(utcNow)
        };

        return price;
    }

    internal Result Update(
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        if (effectiveDate < DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure(CustomerProductErrors.EffectiveDateInThePast);
        }

        if (expiryDate <= DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure(CustomerProductErrors.ExpiryDateInThePastOrToday);
        }

        if (expiryDate <= effectiveDate)
        {
            return Result.Failure(CustomerProductErrors.ExpiryDateBeforeEffectiveDate);
        }

        Comments = comments;
        EffectiveDate = effectiveDate;
        ExpiryDate = expiryDate;
        IsActive = effectiveDate == DateOnly.FromDateTime(utcNow);

        return Result.Success();
    }
}
