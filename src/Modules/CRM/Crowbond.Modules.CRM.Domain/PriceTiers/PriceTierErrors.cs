using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.PriceTiers;

public static class PriceTierErrors
{
    public static Error NotFound(Guid repId) =>
    Error.NotFound("PriceTiers.NotFound", $"The price tier with the identifier {repId} was not found");
}
