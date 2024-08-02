using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Customer.BusinessName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingAddressLine1).NotEmpty().MaximumLength(255);
        RuleFor(c => c.Customer.BillingAddressLine2).MaximumLength(255);
        RuleFor(c => c.Customer.BillingTownCity).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingCounty).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingCountry).MaximumLength(100);
        RuleFor(c => c.Customer.BillingPostalCode).NotEmpty().MaximumLength(16);
        RuleFor(c => c.Customer.CustomerNotes).MaximumLength(500);
        RuleFor(c => c.Customer.CustomerLogo).MaximumLength(100);
    }
}
