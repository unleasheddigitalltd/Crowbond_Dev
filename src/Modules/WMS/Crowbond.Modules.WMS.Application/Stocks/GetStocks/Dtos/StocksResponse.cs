namespace Crowbond.Modules.WMS.Application.Stocks.GetStocks.Dtos;

public sealed record StocksResponse
(
    IReadOnlyCollection<Stock> Stocks,

    Pagination Pagination
);
