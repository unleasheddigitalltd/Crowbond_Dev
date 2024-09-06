using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductStocks;

public sealed record GetProductStocksQuery(Guid ProductId) : IQuery<IReadOnlyCollection<StockResponse>>;
