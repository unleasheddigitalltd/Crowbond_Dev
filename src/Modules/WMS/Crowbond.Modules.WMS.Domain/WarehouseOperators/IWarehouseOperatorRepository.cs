namespace Crowbond.Modules.WMS.Domain.WarehouseOperators;

public interface IWarehouseOperatorRepository
{
    Task<WarehouseOperator?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(WarehouseOperator @operator);
}
