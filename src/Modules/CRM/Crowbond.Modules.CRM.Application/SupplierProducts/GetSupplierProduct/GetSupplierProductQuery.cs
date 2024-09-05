using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProduct;

public sealed record GetSupplierProductQuery(Guid SupplierId, Guid ProductId) : IQuery<SupplierProductResponse>;
