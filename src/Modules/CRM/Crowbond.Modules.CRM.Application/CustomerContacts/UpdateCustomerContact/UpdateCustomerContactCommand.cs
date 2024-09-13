using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;

public sealed record UpdateCustomerContactCommand(Guid CustomerContactId, CustomerContactRequest CustomerContact) : ICommand;
