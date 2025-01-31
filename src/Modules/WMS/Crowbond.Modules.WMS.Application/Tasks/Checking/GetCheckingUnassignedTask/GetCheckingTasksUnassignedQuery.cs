using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingUnassignedTask;

public sealed record GetCheckingTasksUnassignedQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<CheckingTasksResponse>;
