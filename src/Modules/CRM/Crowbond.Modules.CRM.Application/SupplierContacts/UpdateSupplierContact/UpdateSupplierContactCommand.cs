using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;

public sealed record UpdateSupplierContactCommand(Guid UserId, Guid SupplierContactId, SupplierContactRequest SupplierContact) : ICommand;
