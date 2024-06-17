namespace Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;

public sealed record ProductsRequest(

    string Search = "",

    string sort = "name",

    string order = "asc",

    int page = 1,

    int size = 10

);
