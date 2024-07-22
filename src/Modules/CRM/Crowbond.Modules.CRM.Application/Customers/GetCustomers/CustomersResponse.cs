using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers;

public sealed class CustomersResponse : PaginatedResponse<Customer>
{

    public CustomersResponse(IReadOnlyCollection<Customer> Customers, IPagination pagination)
        : base(Customers, pagination)
    { }
}

public sealed record Customer
{
    public Guid Id { get; }
    public string AccountNumber { get; }
    public string BusinessName { get; }
    public string BillingAddressLine1 { get; }
    public string BillingAddressLine2 { get; }
    public string BillingTownCity { get; }
    public string IsActive { get; }
}



