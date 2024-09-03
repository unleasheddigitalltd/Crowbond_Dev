﻿using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Products.GetProducts;

public sealed class ProductsResponse : PaginatedResponse<Product>
{
    public ProductsResponse(IReadOnlyCollection<Product> products, IPagination pagination)
        : base(products, pagination)
    { }
}

public sealed record Product()
{
    public Guid Id { get; }
    public string Sku { get; }
    public string Name { get; }
    public string UnitOfMeasure { get; }
    public string Category { get; }
    public decimal Stock { get; }
    public decimal? ReorderLevel { get; }
    public bool Active { get; }
}
