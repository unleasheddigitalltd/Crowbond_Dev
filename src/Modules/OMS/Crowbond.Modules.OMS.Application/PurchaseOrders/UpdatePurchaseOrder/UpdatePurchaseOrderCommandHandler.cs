using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

internal sealed class UpdatePurchaseOrderCommandHandler(
    IPurchaseOrderHeaderRepository purchaseOrderHeaderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderHeaderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure(PurchaseOrderHeaderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        purchaseOrderHeader.Update(
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
            request.PurchaseOrderHeader.SalesOrderRef,
            request.UserId,
            dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
