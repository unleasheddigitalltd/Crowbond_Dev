﻿using System.Data.Common;

namespace Crowbond.Modules.Warehouse.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
