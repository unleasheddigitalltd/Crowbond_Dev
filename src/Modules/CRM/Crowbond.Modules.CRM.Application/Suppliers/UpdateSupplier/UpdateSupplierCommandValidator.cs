using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;

internal sealed class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierCommandValidator()
    {
        RuleFor(s => s.Supplier.SupplierName).NotEmpty();
        RuleFor(s => s.Supplier.AddressLine1).NotEmpty();
        RuleFor(s => s.Supplier.AddressLine2).NotEmpty();
        RuleFor(s => s.Supplier.AddressTownCity).NotEmpty();
        RuleFor(s => s.Supplier.AddressPostalCode).NotEmpty();
        RuleFor(s => s.Supplier.SupplierEmail).NotEmpty();
        RuleFor(s => s.Supplier.SupplierPhone).NotEmpty();
        RuleFor(s => s.Supplier.SupplierContact).NotEmpty();
        RuleFor(s => s.Supplier.SupplierNotes).MaximumLength(500);
    }
}
