using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Customer.BusinessName).NotEmpty();
        RuleFor(c => c.Customer.ShippingAddressLine1).NotEmpty();
        RuleFor(c => c.Customer.ShippingTownCity).NotEmpty();
        RuleFor(c => c.Customer.ShippingPostalCode).NotEmpty();
        RuleFor(c => c.Customer.ShippingCounty).NotEmpty();
        RuleFor(c => c.Customer.CustomerEmail).NotEmpty();
        RuleFor(c => c.Customer.CustomerPhone).NotEmpty();
        RuleFor(c => c.Customer.CustomerNotes).MaximumLength(500);
    }
}
