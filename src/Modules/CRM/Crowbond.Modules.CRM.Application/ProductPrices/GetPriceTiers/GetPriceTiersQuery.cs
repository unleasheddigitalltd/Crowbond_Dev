using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTiers;

public sealed record GetPriceTiersQuery() : IQuery<IReadOnlyCollection<PriceTierResponse>>;
