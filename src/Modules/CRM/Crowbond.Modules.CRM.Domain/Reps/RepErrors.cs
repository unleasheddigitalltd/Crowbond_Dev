using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Reps;

public static class RepErrors
{
    public static Error NotFound(Guid repId) =>
    Error.NotFound("Reps.NotFound", $"The rep with the identifier {repId} was not found");
}
