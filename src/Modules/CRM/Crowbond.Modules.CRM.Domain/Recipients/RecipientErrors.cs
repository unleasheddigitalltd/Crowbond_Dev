using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Recipients;

public static class RecipientErrors
{
    public static Error NotFound(Guid recipientId) =>
    Error.NotFound("Recipient.NotFound", $"The recipient with the identifier {recipientId} was not found");
}
