using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocks;

public sealed class StocksResponse : PaginatedResponse<Stock>
{
    public StocksResponse(IReadOnlyCollection<Stock> stocks, IPagination pagination)
        : base(stocks, pagination)
    {
    }    
}

public sealed record Stock
{
    public Guid Id { get; }
    public string Sku { get; }
    public string Name { get; }
    public string CategoryName { get; }
    public string Batch { get; }
    public string UnitOfMeasureName { get; }
    public decimal InStock { get; }
    public string Location { get; }
    public int DaysInStock { get; }
    public bool IsActive { get; }
    public string Status { get; }
}
