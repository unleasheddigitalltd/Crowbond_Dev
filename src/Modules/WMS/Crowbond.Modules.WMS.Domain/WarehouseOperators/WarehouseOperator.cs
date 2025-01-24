using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.WarehouseOperators;

public sealed class WarehouseOperator : Entity
{
    private WarehouseOperator() { }

    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }

    public static WarehouseOperator Create(Guid userId)
    {

        var @operator = new WarehouseOperator
        {
            Id = userId,
            IsActive = true,
        };

        return @operator;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
