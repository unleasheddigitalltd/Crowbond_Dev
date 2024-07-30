using Crowbond.Modules.OMS.Domain.Deliveries;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Deliveries;

internal sealed class DeliveryRepository(OmsDbContext context) : IDeliveryRepository
{
    public async Task<Delivery?> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.Deliveries.SingleOrDefaultAsync(d => d.Id == Id, cancellationToken);
    }

    public void Insert(Delivery delivery)
    {
        context.Deliveries.Add(delivery);
    }
}
