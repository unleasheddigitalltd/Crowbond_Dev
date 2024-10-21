using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Products;
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
            supplier.AccountNumber,
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
                supplierProduct.UnitPrice,
                lineItem.Qty,
                (TaxRateType)supplierProduct.TaxRateType,
                lineItem.Comments);

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
