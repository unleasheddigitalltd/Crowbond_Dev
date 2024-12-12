using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLineDetails;

public sealed record GetPutAwayTaskReceiptLineDetailsQuery(Guid UserId, Guid TaskHeaderId, Guid ReceiptLineId) : IQuery<TaskReceiptLineResponse>;
