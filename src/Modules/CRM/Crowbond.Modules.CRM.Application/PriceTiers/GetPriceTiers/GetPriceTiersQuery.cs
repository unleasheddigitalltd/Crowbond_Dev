using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.PriceTiers.GetPriceTiers;

public sealed record GetPriceTiersQuery(): IQuery<IReadOnlyCollection<PriceTierResponse>>;
