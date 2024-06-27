using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStockTransactions;

public sealed record GetStockTransactionsQuery(Guid StockId) : IQuery<IReadOnlyCollection<TransactionResponse>>;
