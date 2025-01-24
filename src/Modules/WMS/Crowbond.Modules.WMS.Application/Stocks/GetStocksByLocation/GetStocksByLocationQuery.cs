using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocksByLocation;

public sealed record GetStocksByLocationQuery(Guid LocationId) : IQuery<IReadOnlyCollection<StockResponse>>;
