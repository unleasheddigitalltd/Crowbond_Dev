using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Recipients;

public sealed class Recipient : Entity
{
    private Recipient()
    {        
    }

    public Guid Id { get; private set; }

    public Guid CustomerContactId { get; private set; }

    public RecipientType RecipientType { get; private set; }

    public static Recipient Create (Guid customerContactId, RecipientType recipientType)
    {
        var recipient = new Recipient
        {
            Id = Guid.NewGuid(),
            CustomerContactId = customerContactId,
            RecipientType = recipientType
        };

        return recipient;
    }

    public void Update (Guid customerContactId, RecipientType recipientType)
    {
        CustomerContactId = customerContactId;
        RecipientType = recipientType;
    }

}
