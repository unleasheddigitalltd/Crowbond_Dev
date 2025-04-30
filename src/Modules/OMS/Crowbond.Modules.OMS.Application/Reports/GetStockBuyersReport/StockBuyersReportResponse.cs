namespace Crowbond.Modules.OMS.Application.Reports.GetStockBuyersReport;

public sealed record StockBuyersReportResponse(List<StockBuyersReportItem> Items);

public sealed record StockBuyersReportItem(
    Guid ProductId,
    string ProductName,
    string ProductSku,
    decimal OrderedQuantity,
    decimal InStockQuantity,
    decimal? ProductPackSize,
    int NeededPacks,
    decimal NeededUnits,
    decimal PurchaseOrderQuantity);
