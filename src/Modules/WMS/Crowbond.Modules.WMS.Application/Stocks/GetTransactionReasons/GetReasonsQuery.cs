using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Stocks.GetTransactionReasons;

public sealed record GetReasonsQuery(string ActionTypeName) : IQuery<IReadOnlyCollection<ReasonResponse>>;
