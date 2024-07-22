using Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutletRoutes;

internal sealed class CustomerOutletRouteRepository(CrmDbContext context) : ICustomerOutletRouteRepository
{
    public async Task<CustomerOutletRoute?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.CustomerOutletRoutes.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(CustomerOutletRoute customerOutletRoute)
    {
        context.Add(customerOutletRoute);
    }
}
