using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayUnassignedTasks;

public sealed record GetPutAwayUnassignedTasksQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<PutAwayTasksResponse>;
