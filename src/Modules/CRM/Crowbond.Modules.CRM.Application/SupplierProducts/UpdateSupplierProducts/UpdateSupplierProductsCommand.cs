using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.UpdateSupplierProducts;

public sealed record UpdateSupplierProductsCommand(Guid SupplierId, IReadOnlyCollection<SupplierProductRequest> SupplierProducts) : ICommand;
