using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;

public sealed record UpdateSupplierCommand(Guid Id, SupplierRequest Supplier) : ICommand;

