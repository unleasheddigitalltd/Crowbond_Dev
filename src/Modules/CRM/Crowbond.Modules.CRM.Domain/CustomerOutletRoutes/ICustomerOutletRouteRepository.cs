namespace Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;

public interface ICustomerOutletRouteRepository
{
    Task<CustomerOutletRoute?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(CustomerOutletRoute customerOutletRoute);
}
