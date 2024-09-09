using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTierPrices;

public sealed record GetPriceTierPricesQuery(Guid PriceTierId) : IQuery<IReadOnlyCollection<ProductPriceResponse>>;
