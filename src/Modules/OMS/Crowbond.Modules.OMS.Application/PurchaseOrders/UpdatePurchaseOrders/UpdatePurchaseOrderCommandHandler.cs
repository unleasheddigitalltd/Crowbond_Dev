using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

internal sealed class UpdatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrder = await purchaseOrderRepository.GetAsync(request.Id, cancellationToken);

        if (purchaseOrder is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(request.Id));
        }

        purchaseOrder.Update(
            supplierName: request.PurchaseOrder.SupplierName,
            shippingAddressLine1: request.PurchaseOrder.ShippingAddressLine1,
            shippingAddressLine2: request.PurchaseOrder.ShippingAddressLine2,
            shippingAddressTownCity: request.PurchaseOrder.ShippingAddressTownCity,
            shippingAddressCounty: request.PurchaseOrder.ShippingAddressCounty,
            shippingAddressCountry: request.PurchaseOrder.ShippingAddressCountry,
            shippingAddressPostalCode: request.PurchaseOrder.ShippingAddressPostalCode,
            supplierContact: request.PurchaseOrder.SupplierContact,
            supplierEmail: request.PurchaseOrder.SupplierEmail,
            supplierPhone: request.PurchaseOrder.SupplierPhone,
            purchaseOrderNotes: request.PurchaseOrder.PurchaseOrderNotes
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
