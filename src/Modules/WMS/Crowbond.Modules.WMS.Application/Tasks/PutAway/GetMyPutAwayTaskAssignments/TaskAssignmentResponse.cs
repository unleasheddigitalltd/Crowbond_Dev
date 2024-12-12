using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetMyPutAwayTaskAssignments;

public sealed record TaskAssignmentResponse(
    Guid Id,
    string TaskNo,
    string ReceiptNo,
    DateOnly ReceivedDate,
    long TotalItems,
    long RegisteredItems,
    TaskAssignmentStatus Status);
