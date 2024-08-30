using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.WarehouseOperators;

internal sealed class WarehouseOperatorRepository(WmsDbContext context) : IWarehouseOperatorRepository
{
    public async Task<WarehouseOperator?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.WarehouseOperators.SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public void Insert(WarehouseOperator @operator)
    {
        context.WarehouseOperators.Add(@operator);
    }
}
