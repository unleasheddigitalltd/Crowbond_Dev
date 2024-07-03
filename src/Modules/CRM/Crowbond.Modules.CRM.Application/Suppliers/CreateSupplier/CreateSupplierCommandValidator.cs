using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;

internal sealed class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierCommandValidator()
    {
        RuleFor(s => s.Supplier.SupplierName).NotEmpty();
        RuleFor(s => s.Supplier.AddressLine1).NotEmpty();
        RuleFor(s => s.Supplier.AddressTownCity).NotEmpty();
        RuleFor(s => s.Supplier.AddressPostalCode).NotEmpty();
        RuleFor(s => s.Supplier.AddressCounty).NotEmpty();
        RuleFor(s => s.Supplier.SupplierEmail).NotEmpty();
        RuleFor(s => s.Supplier.SupplierPhone).NotEmpty();
        RuleFor(s => s.Supplier.SupplierNotes).MaximumLength(500);
    }
}
