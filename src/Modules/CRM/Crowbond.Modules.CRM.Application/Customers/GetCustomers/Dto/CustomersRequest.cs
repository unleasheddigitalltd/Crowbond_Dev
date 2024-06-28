namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto;

public sealed record CustomersRequest(

    string Search = "",

    string sort = "name",

    string order = "asc",

    int page = 1,

    int size = 10

);
