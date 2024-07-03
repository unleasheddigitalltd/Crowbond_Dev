using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplierDetails;

public sealed record GetSupplierDetailsQuery(Guid SupplierId) : IQuery<SupplierDetailsResponse>;

