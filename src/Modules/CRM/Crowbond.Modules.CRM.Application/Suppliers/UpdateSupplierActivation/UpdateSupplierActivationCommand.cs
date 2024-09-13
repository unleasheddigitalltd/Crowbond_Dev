using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplierActivation;

public sealed record UpdateSupplierActivationCommand(Guid SupplierId, bool IsActive) : ICommand;
