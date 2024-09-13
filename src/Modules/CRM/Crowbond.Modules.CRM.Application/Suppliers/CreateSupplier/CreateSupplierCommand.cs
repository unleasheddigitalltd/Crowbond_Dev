using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;

public sealed record CreateSupplierCommand(SupplierRequest Supplier) : ICommand<Guid>;
