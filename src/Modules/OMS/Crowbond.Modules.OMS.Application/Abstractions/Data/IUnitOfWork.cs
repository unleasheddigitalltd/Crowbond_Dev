using System.Data.Common;

namespace Crowbond.Modules.OMS.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
