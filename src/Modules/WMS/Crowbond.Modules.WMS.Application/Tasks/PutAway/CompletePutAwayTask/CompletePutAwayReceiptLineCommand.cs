using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;

public sealed record CompletePutAwayReceiptLineCommand(Guid UserId, Guid TaskHeaderId, Guid ReceiptLineId) : ICommand;
