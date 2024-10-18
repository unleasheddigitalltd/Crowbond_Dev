using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetProductSuggestedStock;

public sealed record GetProductSuggestedStockQuery(Guid ProductId) : IQuery<StockResponse>;
