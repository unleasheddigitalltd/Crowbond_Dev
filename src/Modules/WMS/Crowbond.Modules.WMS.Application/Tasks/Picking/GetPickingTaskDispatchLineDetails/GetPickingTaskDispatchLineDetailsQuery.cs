using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLineDetails;

public sealed record GetPickingTaskDispatchLineDetailsQuery(
    Guid UserId, 
    Guid TaskHeaderId, 
    Guid DispatchLineId) 
    : IQuery<TaskDispatchLineResponse>;
