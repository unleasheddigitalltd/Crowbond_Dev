using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.ConfirmProductPicked;

public sealed record ConfirmProductPickedCommand(
    Guid UserId,
    Guid TaskAssignmentLineId,
    Guid StockId,
    Guid ToLocationId) : ICommand<Guid>;
