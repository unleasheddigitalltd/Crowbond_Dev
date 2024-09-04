using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;

public sealed record CreateWarehouseOperatorCommand(Guid UserId, WarehouseOperatorRequest Operator) : ICommand<Guid>;

