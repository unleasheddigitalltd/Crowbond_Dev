namespace Crowbond.Modules.WMS.Application.Stocks.GetStockTransactions;

public sealed record TransactionResponse(
    Guid Id,
    DateTime TransactionDate,
    string ActionType,
    bool PosAdjustment,
    decimal Quantity,
    string TransactionNote);
