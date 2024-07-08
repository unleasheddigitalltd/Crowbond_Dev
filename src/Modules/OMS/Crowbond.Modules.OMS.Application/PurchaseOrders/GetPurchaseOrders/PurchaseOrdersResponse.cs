using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

public sealed class PurchaseOrdersResponse : PaginatedResponse<PurchaseOrder>
{

    public PurchaseOrdersResponse(IReadOnlyCollection<PurchaseOrder> PurchaseOrders, IPagination pagination)
        : base(PurchaseOrders, pagination)
    { }

}
public sealed record PurchaseOrder
{
    public Guid Id { get; }
    public string PurchaseOrderNo { get; }
    public string SupplierName { get; }
    public string AddressLine1 { get; }
    public string AddressLine2 { get; }
    public string SupplierPhone { get; }
    public string SupplierEmail { get; }
    public string SupplierContact { get; }
}




