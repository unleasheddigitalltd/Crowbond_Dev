using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactPrimary;

public sealed record UpdateSupplierContactPrimaryCommand(Guid UserId, Guid SupplierContactId) : ICommand;
