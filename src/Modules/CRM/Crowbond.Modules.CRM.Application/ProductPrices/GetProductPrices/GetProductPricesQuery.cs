using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.ProductPrices.GetProductPrices;

public sealed record GetProductPricesQuery(Guid ProductId) : IQuery<IReadOnlyCollection<ProductPriceResponse>>;
