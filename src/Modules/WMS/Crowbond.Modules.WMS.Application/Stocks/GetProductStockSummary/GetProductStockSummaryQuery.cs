using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetProductStockSummary;

public sealed record GetProductStockSummaryQuery(Guid ProductId) : IQuery<ProductStockSummaryResponse>;
