using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public static class CustomerOutletErrors
{    public static Error NotFound(Guid customerOutletId) =>
    Error.NotFound("CustomerOutlet.NotFound", $"The customer outlet with the identifier {customerOutletId} was not found");
}
