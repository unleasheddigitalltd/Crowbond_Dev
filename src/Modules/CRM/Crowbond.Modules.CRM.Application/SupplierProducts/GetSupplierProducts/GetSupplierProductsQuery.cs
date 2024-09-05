using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;

public sealed record GetSupplierProductsQuery(Guid SupplierId) : IQuery<IReadOnlyCollection<SupplierProductResponse>>;
