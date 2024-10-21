using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;

internal sealed class CreateCustomerContactCommandValidator : AbstractValidator<CreateCustomerContactCommand>
{
    public CreateCustomerContactCommandValidator()
    {
        RuleFor(t => t.CustomerContact.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.CustomerContact.LastName).NotEmpty().MaximumLength(100);
        RuleFor(t => t.CustomerContact.Username).NotEmpty().MaximumLength(128);
        RuleFor(t => t.CustomerContact.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(t => t.CustomerContact.Mobile).NotEmpty().MaximumLength(20).Matches(@"^(\+44|0)7\d{9}$");
        RuleFor(t => t.CustomerContact.Email).NotEmpty().MaximumLength(255);
    }
}
