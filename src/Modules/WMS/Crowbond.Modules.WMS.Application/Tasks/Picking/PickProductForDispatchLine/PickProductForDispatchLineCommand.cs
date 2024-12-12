using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.PickProductForDispatchLine;

public sealed record PickProductForDispatchLineCommand(
    Guid TaskHeaderId,
    Guid UserId,
    Guid DispatchLineId,
    Guid StockId,
    Guid ToLocationId,
    decimal Qty) : ICommand<Guid>;
