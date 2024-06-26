namespace Crowbond.Modules.WMS.Application.Stocks.GetStock;

public sealed record StockResponse(Guid Id, string Location, decimal OriginalQty, decimal CurrentQty);
