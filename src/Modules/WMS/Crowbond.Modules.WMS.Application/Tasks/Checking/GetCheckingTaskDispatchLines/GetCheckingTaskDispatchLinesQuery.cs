using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingTaskDispatchLines;

public sealed record GetCheckingTaskDispatchLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskDispatchLineResponse>>;
