using Crowbond.Modules.OMS.Domain.DeliveryImages;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.DeliveryImages;

internal sealed class DeliveryImageRepository(OmsDbContext context) : IDeliveryImageRepository
{
    public async Task<DeliveryImage?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.DeliveryImages.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public void Insert(DeliveryImage deliveryImage)
    {
        context.DeliveryImages.Add(deliveryImage);
    }
}
