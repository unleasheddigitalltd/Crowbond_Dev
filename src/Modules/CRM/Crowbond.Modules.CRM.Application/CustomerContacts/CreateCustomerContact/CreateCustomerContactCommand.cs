using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;

public sealed record CreateCustomerContactCommand(Guid CustomerId, Guid UserId, CustomerContactRequest CustomerContact) : ICommand<Guid>;
