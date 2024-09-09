using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.SupplierProducts;
using Crowbond.Modules.OMS.Domain.Suppliers;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

internal sealed class CreatePurchaseOrderCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    ISupplierApi supplierApi,
    ISupplierProductApi supplierProductApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePurchaseOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        SupplierResponse supplier = await supplierApi.GetAsync(request.PurchaseOrder.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure<Guid>(SupplierErrors.NotFound(request.PurchaseOrder.SupplierId));
        }     

        Result<PurchaseOrderHeader> purchaseOrderHeader = PurchaseOrderHeader.Create(
            supplier.Id,
            supplier.SupplierName,
            $"{supplier.FirstName} {supplier.LastName}",
            supplier.PhoneNumber,
            supplier.Email,
            request.PurchaseOrder.RequiredDate,
            request.PurchaseOrder.PurchaseOrderNotes);

        if (purchaseOrderHeader.IsFailure)
        {
            return Result.Failure<Guid>(purchaseOrderHeader.Error);
        }

        foreach (PurchaseOrderRequest.PurchaseOrderLine lineItem in request.PurchaseOrder.PurchaseOrderLines)
        {
            SupplierProductResponse? supplierProduct = await supplierProductApi.GetAsync(request.PurchaseOrder.SupplierId, lineItem.ProductId, cancellationToken);

            if (supplierProduct is null)
            {
                return Result.Failure<Guid>(SupplierProductErrors.NotFound(request.PurchaseOrder.SupplierId, lineItem.ProductId));
            }

            Result result = purchaseOrderHeader.Value.AddLine(
                productId: supplierProduct.ProductId,
                productSku: supplierProduct.ProductSku,
                productName: supplierProduct.ProductName,
                unitOfMeasureName: supplierProduct.UnitOfMeasureName,
                unitPrice: supplierProduct.UnitPrice,
                qty: lineItem.Qty,
                taxRateType: (TaxRateType)supplierProduct.TaxRateType,
                foc: true,
                taxable: true,
                comments: lineItem.Comments);

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }
        }

        purchaseOrderRepository.Insert(purchaseOrderHeader.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return purchaseOrderHeader.Value.Id;
    }

}
