using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProduct : Entity
{
    public CustomerProduct()
    {        
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; }

    public string ProductSku { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public Guid CategoryId { get; private set; }

    public bool IsActive { get; private set; }

    public static CustomerProduct Create(Guid customerId, Guid productId, string productName, string productSku, string unitOfMeasureName, Guid categoryId)
    {
        var customer = new CustomerProduct
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            ProductId = productId,
            ProductName = productName,
            ProductSku = productSku,
            UnitOfMeasureName = unitOfMeasureName,
            CategoryId = categoryId,
            IsActive = true
        };

        return customer;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void Deactive()
    {
        IsActive = false;
    }
}
