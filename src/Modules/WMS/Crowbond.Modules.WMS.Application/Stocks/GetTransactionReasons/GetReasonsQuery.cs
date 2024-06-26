using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Stocks.GetTransactionReasons.Dtos;

namespace Crowbond.Modules.WMS.Application.Stocks.GetTransactionReasons;

public sealed record GetReasonsQuery(string ActionTypeName) : IQuery<IReadOnlyCollection<ReasonResponse>>;
