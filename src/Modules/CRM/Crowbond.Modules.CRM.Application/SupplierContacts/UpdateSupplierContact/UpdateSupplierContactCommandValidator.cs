using FluentValidation;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;

internal sealed class UpdateSupplierContactCommandValidator : AbstractValidator<UpdateSupplierContactCommand>
{
    public UpdateSupplierContactCommandValidator()
    {
        RuleFor(t => t.SupplierContact.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.SupplierContact.LastName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.SupplierContact.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(t => t.SupplierContact.Mobile).MaximumLength(20);
    }
}
