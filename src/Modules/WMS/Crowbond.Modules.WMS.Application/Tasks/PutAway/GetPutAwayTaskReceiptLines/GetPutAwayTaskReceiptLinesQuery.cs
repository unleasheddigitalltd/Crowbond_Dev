using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLines;

public sealed record GetPutAwayTaskReceiptLinesQuery(Guid TaskHeaderId) : IQuery<IReadOnlyCollection<TaskReceiptLineResponse>>;
