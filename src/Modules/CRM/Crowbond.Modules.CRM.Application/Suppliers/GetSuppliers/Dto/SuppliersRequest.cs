namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;

public sealed record SuppliersRequest(

    string Search = "",

    string sort = "name",

    string order = "asc",

    int page = 1,

    int size = 10

);
