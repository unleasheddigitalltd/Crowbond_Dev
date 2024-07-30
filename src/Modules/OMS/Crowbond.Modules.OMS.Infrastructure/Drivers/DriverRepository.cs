using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Drivers;

internal sealed class DriverRepository(OmsDbContext context) : IDriverRepository
{
    public async Task<Driver?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Drivers.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public void Insert(Driver driver)
    {
        context.Drivers.Add(driver);
    }
}
