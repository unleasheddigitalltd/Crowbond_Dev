using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignments;

public sealed record TaskAssignmentResponse(Guid Id, string TaskNo, TaskAssignmentStatus Status);
