using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Products;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.SupplierProducts;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

internal sealed class UpdatePurchaseOrderCommandHandler(
    IPurchaseOrderRepository purchaseOrderHeaderRepository,
    ISupplierProductApi supplierProductApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the Purchase Order Header

        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderHeaderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        // Update the Purchase Order Header Draft
        Result updateResult = purchaseOrderHeader.UpdateDraft(
            requiredDate: request.PurchaseOrder.RequiredDate,
            purchaseOrderNotes: request.PurchaseOrder.PurchaseOrderNotes);

        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        // Remove Existing Line Items
        Result removeResult = purchaseOrderHeader.RemoveLines();

        if (removeResult.IsFailure)
        {
            return removeResult;
        }

        // Add New Line Items
        foreach (PurchaseOrderRequest.PurchaseOrderLine lineItem in request.PurchaseOrder.PurchaseOrderLines)
        {
            SupplierProductResponse? supplierProduct = await supplierProductApi.GetAsync(purchaseOrderHeader.SupplierId, lineItem.ProductId, cancellationToken);

            if (supplierProduct is null)
            {
                return Result.Failure<Guid>(SupplierProductErrors.NotFound(purchaseOrderHeader.SupplierId, lineItem.ProductId));
            }

            Result addLineResult = purchaseOrderHeader.AddLine(
               productId: supplierProduct.Id,
               productSku: supplierProduct.ProductSku,
               productName: supplierProduct.ProductName,
               unitOfMeasureName: supplierProduct.UnitOfMeasureName,
               unitPrice: supplierProduct.UnitPrice,
               qty: lineItem.Qty,
               taxRateType: (TaxRateType)supplierProduct.TaxRateType,
               foc: true,
               taxable: true,
               comments: lineItem.Comments);

            if (addLineResult.IsFailure)
            {
                return Result.Failure<Guid>(addLineResult.Error);
            }
        }

        purchaseOrderHeaderRepository.AddLines(purchaseOrderHeader.Lines);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
