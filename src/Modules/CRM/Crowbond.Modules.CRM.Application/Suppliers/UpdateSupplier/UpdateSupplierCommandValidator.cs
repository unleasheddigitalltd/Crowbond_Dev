using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;

internal sealed class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierCommandValidator()
    {
        RuleFor(s => s.Supplier.SupplierName).NotEmpty().MaximumLength(100);
        RuleFor(s => s.Supplier.AddressLine1).NotEmpty().MaximumLength(255);
        RuleFor(s => s.Supplier.AddressLine2).MaximumLength(255);
        RuleFor(s => s.Supplier.TownCity).NotEmpty().MaximumLength(100);
        RuleFor(s => s.Supplier.PostalCode).NotEmpty().MaximumLength(100);
        RuleFor(s => s.Supplier.County).NotEmpty().MaximumLength(100);
        RuleFor(s => s.Supplier.Country).MaximumLength(100);
        RuleFor(s => s.Supplier.SupplierNotes).MaximumLength(500);
    }
}
