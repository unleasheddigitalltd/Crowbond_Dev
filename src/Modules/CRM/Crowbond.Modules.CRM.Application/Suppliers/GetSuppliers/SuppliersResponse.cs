using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

public sealed class SuppliersResponse : PaginatedResponse<Supplier>
{
    public SuppliersResponse(IReadOnlyCollection<Supplier> Customers, IPagination pagination)
        : base(Customers, pagination)
    { }
}

public sealed record Supplier(
   Guid Id,
   string AccountNumber,
   string SupplierName,
   string AddressLine1,
   string? AddressLine2,
   string TownCity,
   string County,
   string? Country,
   string PostalCode);
