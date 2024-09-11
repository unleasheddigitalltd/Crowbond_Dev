using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;

public sealed record CreateCustomerContactCommand(Guid CustomerId, CustomerContactRequest CustomerContact) : ICommand<Guid>;
