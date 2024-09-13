using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockStatus;

public sealed record UpdateStockStatusCommand(Guid StockId, string StatusType) : ICommand;
