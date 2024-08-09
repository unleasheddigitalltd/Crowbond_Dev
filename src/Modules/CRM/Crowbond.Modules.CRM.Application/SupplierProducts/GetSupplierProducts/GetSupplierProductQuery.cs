using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;

public sealed record GetSupplierProductQuery(Guid SupplierId, Guid ProductId) : IQuery<SupplierProductResponse>;
