using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;

public sealed record AssignPutAwayTaskCommand(Guid TaskHeaderId, Guid WarehouseOperatorId) : ICommand<Guid>;
