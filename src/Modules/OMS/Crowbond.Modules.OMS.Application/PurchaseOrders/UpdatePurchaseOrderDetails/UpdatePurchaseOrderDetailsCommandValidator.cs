using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderDetails;

internal sealed class UpdatePurchaseOrderDetailsCommandValidator : AbstractValidator<UpdatePurchaseOrderDetailsCommand>
{
    public UpdatePurchaseOrderDetailsCommandValidator()
    {
        RuleFor(p => p.PurchaseOrderHeader).NotEmpty();
        RuleFor(p => p.PurchaseOrderHeader.ContactFullName).MaximumLength(200);
        RuleFor(p => p.PurchaseOrderHeader.ContactPhone).MaximumLength(20);
        RuleFor(p => p.PurchaseOrderHeader.ContactEmail).MaximumLength(255);
        RuleFor(p => p.PurchaseOrderHeader.ShippingLocationName).MaximumLength(100);
        RuleFor(p => p.PurchaseOrderHeader.ShippingAddressLine1).MaximumLength(255);
        RuleFor(p => p.PurchaseOrderHeader.ShippingAddressLine2).MaximumLength(255);
        RuleFor(p => p.PurchaseOrderHeader.ShippingTownCity).MaximumLength(100);
        RuleFor(p => p.PurchaseOrderHeader.ShippingCounty).MaximumLength(100);
        RuleFor(p => p.PurchaseOrderHeader.ShippingCountry).MaximumLength(100);
        RuleFor(p => p.PurchaseOrderHeader.ShippingPostalCode).MaximumLength(20);
        RuleFor(p => p.PurchaseOrderHeader.RequiredDate).NotEmpty();
        RuleFor(p => p.PurchaseOrderHeader.SupplierReference).MaximumLength(100);
        RuleFor(p => p.PurchaseOrderHeader.DeliveryCharge).NotEmpty().PrecisionScale(10, 2, true);
        RuleFor(p => p.PurchaseOrderHeader.PurchaseOrderNotes).MaximumLength(500);
        RuleFor(p => p.PurchaseOrderHeader.SalesOrderRef).MaximumLength(100);
    }
}
