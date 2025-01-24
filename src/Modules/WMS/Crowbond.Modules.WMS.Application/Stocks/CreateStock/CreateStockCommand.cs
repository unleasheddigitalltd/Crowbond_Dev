using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.CreateStock;

public sealed record CreateStockCommand(Guid ReceiptLineId, Guid LocationId, decimal Qty) : ICommand<Guid>;
