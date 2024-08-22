using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

public sealed class SuppliersResponse : PaginatedResponse<Supplier>
{
    public SuppliersResponse(IReadOnlyCollection<Supplier> Customers, IPagination pagination)
        : base(Customers, pagination)
    { }
}

public sealed record Supplier
{
   public Guid Id { get; }
   public string AccountNumber { get; }
   public string SupplierName { get; }
   public string AddressLine1 { get; }
   public string? AddressLine2 { get; }
   public string TownCity { get; }
   public string County { get; }
   public string? Country { get; }
   public string PostalCode { get; }
   public string FirstName { get; }
   public string LastName { get; }
   public string PhoneNumber { get; }
   public string Email { get; }
   public bool IsActive { get; }
}
