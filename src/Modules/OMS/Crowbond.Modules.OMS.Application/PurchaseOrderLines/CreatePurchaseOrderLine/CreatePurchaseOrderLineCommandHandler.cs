using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;
using Crowbond.Modules.OMS.Domain.SupplierProducts;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.CreatePurchaseOrderLine;

internal sealed class CreatePurchaseOrderLineCommandHandler(
    IPurchaseOrderHeaderRepository purchaseOrderHeaderRepository,
    IPurchaseOrderLineRepository purchaseOrderLineRepository,
    ISupplierProductApi supplierProductApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePurchaseOrderLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderHeaderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure<Guid>(PurchaseOrderHeaderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        SupplierProductResponse? supplierProduct = await supplierProductApi.GetAsync(purchaseOrderHeader.SupplierId, request.ProductId, cancellationToken);

        if (supplierProduct is null)
        {
            return Result.Failure<Guid>(SupplierProductErrors.NotFound(purchaseOrderHeader.SupplierId, request.ProductId));
        }

        Result<PurchaseOrderLine> result = PurchaseOrderLine.Create(
        productId: request.ProductId,
        productSku: supplierProduct.ProductSku,
        productName: supplierProduct.ProductName,
        unitOfMeasureName: supplierProduct.UnitOfMeasureName,
        unitPrice: supplierProduct.UnitPrice,
        qty: request.Qty,
        taxRateType: (TaxRateType)supplierProduct.TaxRateType,
        foc: true,
        taxable: true,
        comments: request.Comments,
        purchaseOrderHeader: purchaseOrderHeader);

        purchaseOrderLineRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
