using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.RemoveWarehouseOperator;

public sealed record RemoveWarehouseOperatorCommand(Guid UserId) : ICommand;
