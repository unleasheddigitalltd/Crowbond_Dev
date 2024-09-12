using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProduct : Entity, IChangeDetectable
{
    private readonly List<CustomerProductPrice> _price = new();

    private CustomerProduct()
    {
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid ProductId { get; private set; }

    public bool IsActive { get; private set; }
   
    public IReadOnlyCollection<CustomerProductPrice> Price => _price;

    public static Result<CustomerProduct> Create(
        Guid customerId, 
        Guid productId,
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        var customer = new CustomerProduct
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            ProductId = productId,
            IsActive = true,
        };

        Result result = customer.AddPrice(fixedPrice, fixedDiscount, comments, effectiveDate, expiryDate, utcNow);

        if (result.IsFailure)
        {
            return Result.Failure<CustomerProduct>(result.Error);
        }

        return customer;
    }

    public Result Update(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        CustomerProductPrice? activePrice = _price.SingleOrDefault();

        if (activePrice is null)
        {
            // If there is no active price, add a new price if provided
            if (fixedPrice != null || fixedDiscount != null)
            {
                Result result = AddPrice(
                    fixedPrice,
                    fixedDiscount,
                    comments,
                    effectiveDate,
                    expiryDate,
                    utcNow);

                if (result.IsFailure)
                {
                    return Result.Failure<CustomerProduct>(result.Error);
                }
            }
        }
        else
        {
            // If the existing price matches the new price, update other properties
            if (activePrice.FixedPrice == fixedPrice && activePrice.FixedDiscount == fixedDiscount)
            {
                Result result = UpdatePrice(
                    activePrice,
                    comments,
                    effectiveDate,
                    expiryDate,
                    utcNow);

                if (result.IsFailure)
                {
                    return Result.Failure<CustomerProduct>(result.Error);
                }
            }
            else
            {
                // Remove the old price and add the new price
                RemovePrice(activePrice);

                Result result = AddPrice(
                    fixedPrice,
                    fixedDiscount,
                    comments,
                    effectiveDate,
                    expiryDate,
                    utcNow);

                if (result.IsFailure)
                {
                    return Result.Failure<CustomerProduct>(result.Error);
                }
            }
        }

        Activate();
        return Result.Success();
    }

    private Result AddPrice(
        decimal? fixedPrice,
        decimal? fixedDiscount,
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        Result<CustomerProductPrice> result = CustomerProductPrice.Create(
            fixedPrice,
            fixedDiscount,
            comments,
            effectiveDate,
            expiryDate,
            utcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _price.Add(result.Value);
        return Result.Success();
    }

    private Result UpdatePrice(
        CustomerProductPrice price,
        string? comments,
        DateOnly effectiveDate,
        DateOnly? expiryDate,
        DateTime utcNow)
    {
        return price.Update(comments, effectiveDate, expiryDate, utcNow);
    }

    private void RemovePrice(CustomerProductPrice price)
    {
        _price.Remove(price);
    }


    public void Activate()
    {
        IsActive = true;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        _price.Clear();
    }
}
