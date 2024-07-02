using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Customer.BusinessName).NotEmpty();
        RuleFor(c => c.Customer.ShippingAddressLine1).NotEmpty();
        RuleFor(c => c.Customer.ShippingAddressLine2).NotEmpty();
        RuleFor(c => c.Customer.ShippingTownCity).NotEmpty();
        RuleFor(c => c.Customer.ShippingPostalCode).NotEmpty();
        RuleFor(c => c.Customer.CustomerEmail).NotEmpty();
        RuleFor(c => c.Customer.CustomerPhone).NotEmpty();
        RuleFor(c => c.Customer.CustomerContact).NotEmpty();
    }
}
