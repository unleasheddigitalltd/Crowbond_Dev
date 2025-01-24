using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.AddWarehouseOperator;

public sealed record AddWarehouseOperatorCommand(Guid UserId) : ICommand;

