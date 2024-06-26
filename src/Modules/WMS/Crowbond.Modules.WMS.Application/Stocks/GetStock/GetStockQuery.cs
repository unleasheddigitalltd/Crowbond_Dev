using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStock;

public sealed record GetStockQuery(Guid StockId) : IQuery<StockResponse>;
