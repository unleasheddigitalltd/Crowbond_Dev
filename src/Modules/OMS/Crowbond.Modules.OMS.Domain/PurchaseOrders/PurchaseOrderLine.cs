using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrders;

public sealed class PurchaseOrderLine
{
    private PurchaseOrderLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid PurchaseOrderHeaderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductSku { get; private set; }

    public string ProductName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Qty { get; private set; }

    public decimal SubTotal { get; private set; }

    public TaxRateType TaxRateType { get; private set; }

    public decimal Tax { get; private set; }

    public decimal LineTotal { get; private set; }

    public bool FOC { get; private set; }

    public bool Taxable { get; private set; }

    public string? Comments { get; private set; }

    internal static Result<PurchaseOrderLine> Create(
        Guid productId,
        string productSku,
        string productName,
        string unitOfMeasureName,
        decimal unitPrice,
        decimal qty,
        TaxRateType taxRateType,
        bool foc,
        bool taxable,
        string? comments)
    {
        var purchaseOrderLine = new PurchaseOrderLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductSku = productSku,
            ProductName = productName,
            UnitOfMeasureName = unitOfMeasureName,
            UnitPrice = unitPrice,
            Qty = qty,
            TaxRateType = taxRateType,
            FOC = foc,
            Taxable = taxable,
            Comments = comments
        };

        purchaseOrderLine.SubTotal = purchaseOrderLine.UnitPrice * purchaseOrderLine.Qty;
        purchaseOrderLine.Tax = purchaseOrderLine.Taxable ? purchaseOrderLine.SubTotal * purchaseOrderLine.GetTaxRate(purchaseOrderLine.TaxRateType) : 0;
        purchaseOrderLine.LineTotal = purchaseOrderLine.SubTotal + purchaseOrderLine.Tax;

        return purchaseOrderLine;
    }

    internal void Update(
        decimal qty,
        string? comments)
    {
        Qty = qty;
        Comments = comments;

        SubTotal = UnitPrice * Qty;
        Tax = Taxable ? SubTotal * GetTaxRate(TaxRateType) : 0;
        LineTotal = SubTotal + Tax;
    }

    private decimal GetTaxRate(TaxRateType taxRateType)
    {
        return taxRateType switch
        {
            TaxRateType.VatOnIncome => 0.2m,
            TaxRateType.NoVat => 0,
            TaxRateType.ZeroRatedIncome => 0,
            _ => 0
        };
    }
}
