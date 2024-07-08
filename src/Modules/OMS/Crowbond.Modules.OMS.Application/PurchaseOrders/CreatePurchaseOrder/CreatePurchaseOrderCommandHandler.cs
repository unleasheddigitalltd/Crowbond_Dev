using System.Xml.Linq;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

internal sealed class CreatePurchaseOrderCommandHandler(
        IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePurchaseOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        

        Result<PurchaseOrderHeader> result = PurchaseOrderHeader.Create(
            "1",
            request.PurchaseOrder.PaidBy, 
            request.PurchaseOrder.PaidDate,
            request.PurchaseOrder.SupplierId,
             request.PurchaseOrder.SupplierName,
             request.PurchaseOrder.SupplierEmail,
             request.PurchaseOrder.SupplierPhone,  
             request.PurchaseOrder.SupplierContact,
             request.PurchaseOrder.PurchaseOrderAmount,
             request.PurchaseOrder.ShippingAddressLine1,
             request.PurchaseOrder.ShippingAddressLine2,
             request.PurchaseOrder.ShippingAddressTownCity,
             request.PurchaseOrder.ShippingAddressCounty,
             request.PurchaseOrder.ShippingAddressCountry,
             request.PurchaseOrder.ShippingAddressPostalCode,
             request.PurchaseOrder.RequiredDate,
             request.PurchaseOrder.PurchaseDate,
             request.PurchaseOrder.ExpectedShippingDate,
             request.PurchaseOrder.SupplierReference,
             request.PurchaseOrder.PurchaseOrderTax, 
             request.PurchaseOrder.DeliveryMethod, 
             request.PurchaseOrder.DeliveryCharge,  
             request.PurchaseOrder.PaymentMethod,
             request.PurchaseOrder.PaymentStatus,
             request.PurchaseOrder.PurchaseOrderNotes,
             request.PurchaseOrder.SalesOrderRef,
             request.PurchaseOrder.Tags
            );



        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        purchaseOrderRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
