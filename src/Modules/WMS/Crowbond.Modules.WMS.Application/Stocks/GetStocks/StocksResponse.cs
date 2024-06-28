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
    public string Category { get; }
    public string Batch { get; }
    public string UnitOfMeasure { get; }
    public decimal InStock { get; }
    public decimal Available { get; }
    public decimal Allocated { get; }
    public decimal OnHold { get; }
    public string Location { get; }
    public decimal ReorderLevel { get; }
    public int DaysInStock { get; }
    public DateTime SellByDate { get; }
    public DateTime UseByDate { get; }
    public bool Active { get; }
}
