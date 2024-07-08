using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

public sealed class SuppliersResponse : PaginatedResponse<Supplier>
{

    public SuppliersResponse(IReadOnlyCollection<Supplier> Suppliers, IPagination pagination)
        : base(Suppliers, pagination)
    { }

}
public sealed record Supplier
{
    public Guid Id { get; }
    public string AccountNumber { get; }
    public string SupplierName { get; }
    public string AddressLine1 { get; }
    public string AddressLine2 { get; }
    public string SupplierPhone { get; }
    public string SupplierEmail { get; }
    public string SupplierContact { get; }
}




