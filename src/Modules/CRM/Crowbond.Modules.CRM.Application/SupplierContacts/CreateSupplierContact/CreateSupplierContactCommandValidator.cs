using FluentValidation;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.CreateSupplierContact;

internal sealed class CreateSupplierContactCommandValidator : AbstractValidator<CreateSupplierContactCommand>
{
    public CreateSupplierContactCommandValidator()
    {
        RuleFor(t => t.SupplierContact.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.SupplierContact.LastName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.SupplierContact.Username).NotEmpty().MaximumLength(128);
        RuleFor(t => t.SupplierContact.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(t => t.SupplierContact.Mobile).MaximumLength(20);
        RuleFor(t => t.SupplierContact.Email).NotEmpty().MaximumLength(255);
    }
}
