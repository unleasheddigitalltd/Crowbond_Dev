using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.GerWarehouseOperator;

public sealed record GetWarehouseOperatorQuery(Guid OperatorId) : IQuery<WarehouseOperatorResponse>;
