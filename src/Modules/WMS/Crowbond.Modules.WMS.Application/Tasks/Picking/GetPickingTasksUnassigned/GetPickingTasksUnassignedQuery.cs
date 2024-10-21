using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTasksUnassigned;

public sealed record GetPickingTasksUnassignedQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<PickingTasksResponse>;
