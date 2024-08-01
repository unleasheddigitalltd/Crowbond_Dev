using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;

public sealed record UpdateCustomerContactCommand(Guid UserId, Guid CustomerContactId, CustomerContactRequest CustomerContact) : ICommand;
