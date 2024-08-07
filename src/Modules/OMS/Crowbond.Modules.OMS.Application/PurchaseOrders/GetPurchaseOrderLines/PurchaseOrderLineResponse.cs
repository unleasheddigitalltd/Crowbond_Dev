namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;

public sealed record PurchaseOrderLineResponse {
    public Guid Id { get; }
    public Guid PurchaseOrderHeaderId { get; }
    public Guid ProductId { get; }
    public string ProductSku { get; }
    public string ProductName { get; }
    public string UnitOfMeasureName { get; }
    public decimal UnitPrice { get; }
    public decimal Qty { get; }
    public decimal SubTotal { get; }
    public decimal Tax { get; }
    public decimal LineTotal { get; }
    public bool FOC { get; }
    public bool Taxable { get; }
    public string? Comments { get; }
}
