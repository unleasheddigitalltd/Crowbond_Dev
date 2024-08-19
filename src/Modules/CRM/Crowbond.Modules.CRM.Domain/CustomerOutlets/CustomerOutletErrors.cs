using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerOutlets;

public static class CustomerOutletErrors
{    public static Error NotFound(Guid customerOutletId) =>
    Error.NotFound("CustomerOutlet.NotFound", $"The customer outlet with the identifier {customerOutletId} was not found");

    public static Error InvalitTimeFormat(string property) =>
    Error.Problem("CustomerOutlet.InvalitTimeFormat", $"Invalid time format in {property}. Expected format is HH:mm or HH:mm:ss.");
}
