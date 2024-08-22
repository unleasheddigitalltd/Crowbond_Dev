using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;

public sealed record CreateSupplierCommand(Guid UserId, SupplierRequest Supplier) : ICommand<Guid>;
