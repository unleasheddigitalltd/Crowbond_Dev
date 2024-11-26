using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Products;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.SupplierProducts;
using Crowbond.Modules.OMS.Domain.Suppliers;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLineToPo;

internal sealed class AddOrderLineToPoCommandHandler(
    IOrderRepository orderRepository,
    InventoryService inventoryService,
    IPurchaseOrderRepository purchaseOrderRepository,
    ISupplierApi supplierApi,
    ISupplierProductApi supplierProductApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddOrderLineToPoCommand>
{
    public async Task<Result> Handle(AddOrderLineToPoCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(request.OrderLineId));
        }

        OrderHeader? order = await orderRepository.GetAsync(orderLine.OrderHeaderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(orderLine.OrderHeaderId));
        }

        decimal availableQty = await inventoryService.GetAvailableQuantityAsync(orderLine.ProductId, cancellationToken);

        if (availableQty >= orderLine.OrderedQty)
        {
            return Result.Failure(OrderErrors.NoShortage);
        }

        decimal shortageQty = orderLine.OrderedQty - availableQty;

        SupplierResponse supplier = await supplierApi.GetAsync(request.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure<Guid>(SupplierErrors.NotFound(request.SupplierId));
        }

        SupplierProductResponse? supplierProduct = await supplierProductApi.GetAsync(request.SupplierId, orderLine.ProductId, cancellationToken);

        if (supplierProduct is null)
        {
            return Result.Failure<Guid>(SupplierProductErrors.NotFound(request.SupplierId, orderLine.ProductId));
        }

        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderRepository.GetDraftBySupplierIdAsync(request.SupplierId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            // Create new po with line
            Result<PurchaseOrderHeader> poHeaderResult = PurchaseOrderHeader.Create(
            supplier.Id,
            supplier.AccountNumber,
            supplier.SupplierName,
            $"{supplier.FirstName} {supplier.LastName}",
            supplier.PhoneNumber,
            supplier.Email,
            order.ShippingDate,
            null);

            if (poHeaderResult.IsFailure || poHeaderResult.Value is null)
            {
                return Result.Failure<Guid>(poHeaderResult.Error);
            }

            purchaseOrderRepository.Insert(poHeaderResult.Value);

            purchaseOrderHeader = poHeaderResult.Value;
        }
        else
        {
            // Update the required date of existing po 
            purchaseOrderHeader.UpdateRequiredDate(order.ShippingDate);
        }

        //  add new po line or update po line qty based on shortage qty.

        Result<PurchaseOrderLine> poLineResult = purchaseOrderHeader.AddLine(
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
        shortageQty,
        (TaxRateType)supplierProduct.TaxRateType,
        null);

        if (poLineResult.IsFailure)
        {
            return Result.Failure<Guid>(poLineResult.Error);
        }

        purchaseOrderRepository.AddLine(poLineResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
