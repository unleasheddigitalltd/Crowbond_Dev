using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProductsWithStock;

public sealed record GetSupplierProductsWithStockQuery(Guid SupplierId) : IQuery<IReadOnlyCollection<ProductWithStockResponse>>;
