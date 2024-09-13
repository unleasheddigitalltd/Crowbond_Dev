using System.Xml.Linq;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProduct : Entity, IChangeDetectable
{
    private readonly List<CustomerProductPriceHistory> _priceHistory = new();

    private CustomerProduct()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal? FixedPrice { get; private set; }

    public decimal? FixedDiscount { get; private set; }

    public string? Comments { get; private set; }

    public DateOnly? EffectiveDate { get; private set; }

    public DateOnly? ExpiryDate { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyCollection<CustomerProductPriceHistory> PriceHistory => _priceHistory;

    public static Result<CustomerProduct> Create(
        Guid customerId,
        Guid productId,
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly? effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {

        Result result = InputValidate(
            fixedPrice,
            fixedDiscount,
            effectiveDate,
            expiryDate,
            utcNow);

        if (result.IsFailure)
        {
            return Result.Failure<CustomerProduct>(result.Error);
        }

        var customerProduct = new CustomerProduct
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            ProductId = productId,
            FixedPrice = fixedPrice,
            FixedDiscount = fixedDiscount,
            Comments = comments,
            EffectiveDate = effectiveDate,
            ExpiryDate = expiryDate,
            IsActive = true,
        };


        var newPriceHistory = new CustomerProductPriceHistory(
            fixedPrice,
            fixedDiscount,
            comments,
            effectiveDate,
            expiryDate,
            utcNow,
            true);

        customerProduct.AddPriceHistory(newPriceHistory);

        return customerProduct;
    }

    public Result<CustomerProductPriceHistory> Update(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly? effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        Result result = InputValidate(
            fixedPrice,
            fixedDiscount,
            effectiveDate,
            expiryDate,
            utcNow);

        if (result.IsFailure)
        {
            return Result.Failure<CustomerProductPriceHistory>(result.Error);
        }

        FixedPrice = fixedPrice;
        FixedDiscount = fixedDiscount;
        Comments = comments;
        EffectiveDate = effectiveDate;
        ExpiryDate = expiryDate;
        IsActive = true;

        var newPriceHistory = new CustomerProductPriceHistory(
            fixedPrice,
            fixedDiscount,
            comments,
            effectiveDate,
            expiryDate,
            utcNow,
            true);

        AddPriceHistory(newPriceHistory);

        return Result.Success(newPriceHistory);
    }

    public CustomerProductPriceHistory Deactivate(DateTime utcNow)
    {
        FixedPrice = null;
        FixedDiscount = null;
        Comments = null;
        EffectiveDate = null;
        ExpiryDate = null;
        IsActive = false;
        var newPriceHistory = new CustomerProductPriceHistory(
            null,
            null,
            null,
            null,
            null,
            utcNow,
            false);

        AddPriceHistory(newPriceHistory);

        return newPriceHistory;
    }

    private static Result InputValidate(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        DateOnly? effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        if (effectiveDate is not null && fixedDiscount is null && fixedPrice is null)
        {
            return Result.Failure(CustomerProductErrors.EffectiveDateWithoutPricing);
        }

        if (fixedDiscount is not null && fixedPrice is not null)
        {
            return Result.Failure(CustomerProductErrors.FixedDiscountAndFixedPriceConflict);
        }

        if (effectiveDate is null && (fixedDiscount is not null || fixedPrice is not null))
        {
            return Result.Failure(CustomerProductErrors.EffectiveDateIsNull);
        }

        if (effectiveDate is not null && effectiveDate < DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure(CustomerProductErrors.EffectiveDateInThePast);
        }

        if (expiryDate is not null && expiryDate <= DateOnly.FromDateTime(utcNow))
        {
            return Result.Failure(CustomerProductErrors.ExpiryDateInThePastOrToday);
        }

        if (effectiveDate is not null && expiryDate <= effectiveDate)
        {
            return Result.Failure(CustomerProductErrors.ExpiryDateBeforeEffectiveDate);
        }

        return Result.Success();
    }

    public void AddPriceHistory(CustomerProductPriceHistory priceHistory)
    {
        _priceHistory.Add(priceHistory);
    }
}
