using System.Xml.Linq;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

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

    public decimal Tax { get; private set; }

    public decimal LineTotal { get; private set; }

    public bool FOC { get; private set; }

    public bool Taxable { get; private set; }

    public string? Comments { get; private set; }


    public static Result<PurchaseOrderLine> Create(
        Guid purchaseOrderHeaderId,
        Guid productId,
        string productSku,
        string productName,
        string unitOfMeasureName,
        decimal unitPrice,
        int qty,
        decimal subTotal,
        decimal tax,
        decimal lineTotal,
        bool foc,
        bool taxable,
        string? comments)
    {
        var purchaseOrderLine = new PurchaseOrderLine
        {
            Id = Guid.NewGuid(),
            PurchaseOrderHeaderId = purchaseOrderHeaderId,
            ProductId = productId,
            ProductSku = productSku,
            ProductName = productName,
            UnitOfMeasureName = unitOfMeasureName,
            UnitPrice = unitPrice,
            Qty = qty,
            SubTotal = subTotal,
            Tax = tax,
            LineTotal = lineTotal,
            FOC = foc,
            Taxable = taxable,
            Comments = comments
        };

        return purchaseOrderLine;
    }

    public void Update(
        int qty,
        decimal subTotal,
        decimal tax,
        decimal lineTotal,
        string? comments)
    {
        Qty = qty;
        SubTotal = subTotal;
        Tax = tax;
        LineTotal = lineTotal;
        Comments = comments;
    }

}
