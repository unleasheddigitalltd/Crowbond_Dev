using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockLocation;

public sealed record UpdateStockLocationCommand(Guid UserId, Guid StockId, string TransactionNote, Guid ReasonId, decimal Quantity, Guid Destination) : ICommand<Guid>;
