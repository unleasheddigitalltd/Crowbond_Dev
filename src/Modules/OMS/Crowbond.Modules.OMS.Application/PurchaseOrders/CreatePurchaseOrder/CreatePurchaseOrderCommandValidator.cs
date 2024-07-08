using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

internal sealed class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator()
    {
        RuleFor(s => s.PurchaseOrder.SupplierName).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressLine1).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressTownCity).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressPostalCode).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressCounty).NotEmpty();
        RuleFor(s => s.PurchaseOrder.SupplierEmail).NotEmpty();
        RuleFor(s => s.PurchaseOrder.SupplierPhone).NotEmpty();
        RuleFor(s => s.PurchaseOrder.PurchaseOrderNotes).MaximumLength(500);
    }
}
