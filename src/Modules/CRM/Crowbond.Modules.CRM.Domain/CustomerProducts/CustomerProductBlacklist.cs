using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public sealed class CustomerProductBlacklist: Entity, ISoftDeletable, IAuditable
{
    private CustomerProductBlacklist()
    {        
    }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid ProductId { get; private set; }
    public string? Comments { get; private set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public static CustomerProductBlacklist Create(Guid customerId, Guid productId, string? comments)
    {
        var blacklist = new CustomerProductBlacklist
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            CustomerId = customerId,
            Comments = comments
        };

        return blacklist;
    }

    public void Update(string? comments)
    {
        Comments = comments;
    }
}
