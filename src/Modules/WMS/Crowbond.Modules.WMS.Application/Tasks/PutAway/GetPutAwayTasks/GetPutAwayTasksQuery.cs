using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTasks;

public sealed record GetPutAwayTasksQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<PutAwayTasksResponse>;
