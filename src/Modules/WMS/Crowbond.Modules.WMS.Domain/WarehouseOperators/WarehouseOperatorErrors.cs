using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.WarehouseOperators;

public static class WarehouseOperatorErrors
{
    public static Error NotFound(Guid operatorId) =>
    Error.NotFound("Operator.NotFound", $"The operator with the identifier {operatorId} was not found");

}
