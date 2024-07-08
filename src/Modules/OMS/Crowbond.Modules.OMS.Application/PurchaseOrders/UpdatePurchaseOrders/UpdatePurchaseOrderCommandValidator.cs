using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

internal sealed class UpdatePurchaseOrderCommandValidator : AbstractValidator<UpdatePurchaseOrderCommand>
{
    public UpdatePurchaseOrderCommandValidator()
    {
        RuleFor(s => s.PurchaseOrder.SupplierName).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressLine1).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressLine2).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressTownCity).NotEmpty();
        RuleFor(s => s.PurchaseOrder.ShippingAddressPostalCode).NotEmpty();
        RuleFor(s => s.PurchaseOrder.SupplierEmail).NotEmpty();
        RuleFor(s => s.PurchaseOrder.SupplierPhone).NotEmpty();
        RuleFor(s => s.PurchaseOrder.SupplierContact).NotEmpty();
        RuleFor(s => s.PurchaseOrder.PurchaseOrderNotes).MaximumLength(500);
    }
}
