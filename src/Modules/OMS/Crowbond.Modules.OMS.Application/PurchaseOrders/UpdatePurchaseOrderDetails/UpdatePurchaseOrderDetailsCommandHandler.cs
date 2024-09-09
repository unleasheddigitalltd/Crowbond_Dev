using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderDetails;

internal sealed class UpdatePurchaseOrderDetailsCommandHandler(
    IPurchaseOrderRepository purchaseOrderHeaderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderDetailsCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderHeaderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        purchaseOrderHeader.UpdateDetails(
            request.PurchaseOrderHeader.ContactFullName,
            request.PurchaseOrderHeader.ContactPhone,
            request.PurchaseOrderHeader.ContactEmail,
            request.PurchaseOrderHeader.ShippingLocationName,
            request.PurchaseOrderHeader.ShippingAddressLine1,
            request.PurchaseOrderHeader.ShippingAddressLine2,
            request.PurchaseOrderHeader.ShippingTownCity,
            request.PurchaseOrderHeader.ShippingCounty,
            request.PurchaseOrderHeader.ShippingCountry,
            request.PurchaseOrderHeader.ShippingPostalCode,
            request.PurchaseOrderHeader.RequiredDate,
            request.PurchaseOrderHeader.ExpectedShippingDate,
            request.PurchaseOrderHeader.SupplierReference,
            request.PurchaseOrderHeader.DeliveryMethod,
            request.PurchaseOrderHeader.DeliveryCharge,
            request.PurchaseOrderHeader.PaymentMethod,
            request.PurchaseOrderHeader.PurchaseOrderNotes,
            request.PurchaseOrderHeader.SalesOrderRef);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
