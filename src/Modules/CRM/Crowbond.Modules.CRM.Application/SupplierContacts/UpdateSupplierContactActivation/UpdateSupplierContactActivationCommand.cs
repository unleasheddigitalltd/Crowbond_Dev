using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactActivation;

public sealed record UpdateSupplierContactActivationCommand(Guid SupplierContactId, bool IsActive) : ICommand;
