using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockLocation;

public sealed record UpdateStockLocationCommand(Guid StockId, string TransactionNote, Guid ReasonId, decimal Quantity, Guid Destination) : ICommand;
