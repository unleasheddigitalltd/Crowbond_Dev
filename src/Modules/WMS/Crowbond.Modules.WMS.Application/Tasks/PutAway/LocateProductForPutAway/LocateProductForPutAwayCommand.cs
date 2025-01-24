using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.LocateProductForPutAway;

public sealed record LocateProductForPutAwayCommand(Guid UserId, Guid TaskHeaderId, Guid ReceiptLineId, Guid LocationId, decimal Qty) : ICommand<Guid>;
