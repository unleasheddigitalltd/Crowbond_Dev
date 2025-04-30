using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Products;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.SupplierProducts;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.AddPurchaseOrderLine;

internal sealed class AddPurchaseOrderLineCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    ISupplierProductApi supplierProductApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddPurchaseOrderLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddPurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        var purchaseOrderHeader = await purchaseOrderRepository.GetAsync(request.PurchaseOrderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure<Guid>(PurchaseOrderErrors.NotFound(request.PurchaseOrderId));
        }

        var supplierProduct = await supplierProductApi.GetAsync(purchaseOrderHeader.SupplierId, request.ProductId, cancellationToken);

        if (supplierProduct is null)
        {
            return Result.Failure<Guid>(SupplierProductErrors.NotFound(purchaseOrderHeader.SupplierId, request.ProductId));
        }

        Result<PurchaseOrderLine> result = purchaseOrderHeader.AddLine(
            supplierProduct.ProductId,
            supplierProduct.ProductSku,
            supplierProduct.ProductName,
            supplierProduct.UnitOfMeasureName,
            supplierProduct.CategoryId,
            supplierProduct.CategoryName,
            supplierProduct.BrandId,
            supplierProduct.BrandName,
            supplierProduct.ProductGroupId,
            supplierProduct.ProductGroupName,
            request.UnitPrice,
            request.Qty,
            (TaxRateType)supplierProduct.TaxRateType,
            request.Comments);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        purchaseOrderRepository.AddLine(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
