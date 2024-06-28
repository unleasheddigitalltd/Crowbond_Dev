using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStockDetails;
public sealed record GetStockDetailsQuery(Guid StockId) : IQuery<StockDetailsResponse>;
