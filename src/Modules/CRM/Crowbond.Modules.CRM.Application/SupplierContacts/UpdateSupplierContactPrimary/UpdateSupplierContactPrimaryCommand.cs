using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactPrimary;

public sealed record UpdateSupplierContactPrimaryCommand(Guid SupplierContactId) : ICommand;
