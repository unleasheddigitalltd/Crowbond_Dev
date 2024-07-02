using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplier;

public sealed record GetSupplierQuery(Guid SupplierId) : IQuery<SupplierResponse>;

