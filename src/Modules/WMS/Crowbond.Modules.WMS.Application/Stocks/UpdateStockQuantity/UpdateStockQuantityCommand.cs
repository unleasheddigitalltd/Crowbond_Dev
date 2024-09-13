using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockQuantity;

public sealed record UpdateStockQuantityCommand(Guid StockId, string TransactionNote, Guid ReasonId, decimal Quantity) : ICommand;
