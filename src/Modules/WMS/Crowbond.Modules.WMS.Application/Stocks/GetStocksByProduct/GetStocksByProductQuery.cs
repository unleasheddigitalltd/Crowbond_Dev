using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocksByProduct;

public sealed record GetStocksByProductQuery(Guid ProductId) : IQuery<IReadOnlyCollection<StockResponse>>;
