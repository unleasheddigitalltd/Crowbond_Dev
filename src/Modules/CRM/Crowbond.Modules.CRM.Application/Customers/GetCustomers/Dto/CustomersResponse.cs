using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto;

public sealed class CustomersResponse : PaginatedResponse<Customer>
{

    public CustomersResponse(IReadOnlyCollection<Customer> Customers, IPagination pagination)
        : base(Customers, pagination)
    { }


}


