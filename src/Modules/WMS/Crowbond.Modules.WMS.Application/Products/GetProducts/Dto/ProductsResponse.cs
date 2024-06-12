namespace Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;

public sealed record ProductsResponse(

    IReadOnlyCollection<Product> Products,

    Pagination Pagination
);

