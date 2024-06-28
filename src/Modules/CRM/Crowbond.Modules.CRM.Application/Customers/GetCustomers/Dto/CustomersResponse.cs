namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto;

public sealed record CustomersResponse(

    IReadOnlyCollection<Customer> Customers,

    Pagination Pagination
);

