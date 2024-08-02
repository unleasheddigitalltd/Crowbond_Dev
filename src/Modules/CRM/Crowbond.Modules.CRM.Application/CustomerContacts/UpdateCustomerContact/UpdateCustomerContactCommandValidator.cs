using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;
internal sealed class UpdateCustomerContactCommandValidator : AbstractValidator<UpdateCustomerContactCommand>
{
    public UpdateCustomerContactCommandValidator()
    {
        RuleFor(t => t.CustomerContact.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.CustomerContact.LastName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.CustomerContact.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(t => t.CustomerContact.Mobile).MaximumLength(20);
    }
}
