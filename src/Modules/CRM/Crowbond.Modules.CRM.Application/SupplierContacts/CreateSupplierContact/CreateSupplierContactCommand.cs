using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.CreateSupplierContact;

public sealed record CreateSupplierContactCommand(Guid UserId, Guid SupplierId, SupplierContactRequest SupplierContact) : ICommand<Guid>;
