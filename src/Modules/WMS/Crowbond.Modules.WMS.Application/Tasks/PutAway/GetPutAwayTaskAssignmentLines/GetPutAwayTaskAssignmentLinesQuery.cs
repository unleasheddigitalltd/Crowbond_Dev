using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignmentLines;

public sealed record GetPutAwayTaskAssignmentLinesQuery(Guid WarehouseOperatorId, Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskAssignmentLineResponse>>;
