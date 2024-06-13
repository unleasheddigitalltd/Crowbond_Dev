using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetProductStocks.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetProductStocks;

public sealed record GetProductStocksQuery(Guid ProductId) : IQuery<IReadOnlyCollection<StockResponse>>;
