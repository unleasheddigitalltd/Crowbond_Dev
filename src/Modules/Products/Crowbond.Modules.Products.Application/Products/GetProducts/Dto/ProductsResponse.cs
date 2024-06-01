namespace Crowbond.Modules.Products.Application.Products.GetProducts.Dto;

public sealed record ProductsResponse(

    IReadOnlyCollection<Product> Products,

    Pagination Pagination
);

