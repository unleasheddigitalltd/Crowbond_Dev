using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Stocks.GetStocks.Dtos;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocks;

public sealed record GetStocksQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<StocksResponse>;
