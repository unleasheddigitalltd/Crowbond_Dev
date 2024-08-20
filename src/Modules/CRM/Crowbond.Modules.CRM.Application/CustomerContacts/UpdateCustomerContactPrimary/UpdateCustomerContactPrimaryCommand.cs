using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactPrimary;

public sealed record UpdateCustomerContactPrimaryCommand(Guid UserId, Guid CustomerContactId) : ICommand;
