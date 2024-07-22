using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Customer.BusinessName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingAddressLine1).NotEmpty().MaximumLength(255);
        RuleFor(c => c.Customer.BillingAddressLine2).MaximumLength(255);
        RuleFor(c => c.Customer.BillingTownCity).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingCounty).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Customer.BillingCountry).MaximumLength(100);
        RuleFor(c => c.Customer.BillingPostalCode).NotEmpty().MaximumLength(16);
        RuleFor(c => c.Customer.Discount).NotEmpty().PrecisionScale(5, 2, false);
        RuleFor(c => c.Customer.DeliveryMinOrderValue).PrecisionScale(10, 2, false);
        RuleFor(c => c.Customer.DeliveryCharge).PrecisionScale(10, 2, false);
        RuleFor(c => c.Customer.CustomerNotes).MaximumLength(500);

        RuleForEach(c => c.Customer.CustomerContacts)
            .ChildRules(t =>
            {
                t.RuleFor(t => t.FirstName).NotEmpty().MaximumLength(100);
                t.RuleFor(t => t.LastName).NotEmpty().MaximumLength(100);
                t.RuleFor(t => t.Username).NotEmpty().MaximumLength(128);
                t.RuleFor(t => t.PhoneNumber).NotEmpty().MaximumLength(20);
                t.RuleFor(t => t.Mobile).NotEmpty().MaximumLength(20);
                t.RuleFor(t => t.Email).NotEmpty().MaximumLength(255);
            });

        RuleForEach(c => c.Customer.CustomerOutletAddresses)
            .ChildRules(s =>
            {
                s.RuleFor(s => s.LocationName).NotEmpty().MaximumLength(100);
                s.RuleFor(s => s.FullName).NotEmpty().MaximumLength(100);
                s.RuleFor(s => s.Email).MaximumLength(255);
                s.RuleFor(s => s.PhoneNumber).NotEmpty().MaximumLength(20);
                s.RuleFor(s => s.Mobile).MaximumLength(20);
                s.RuleFor(s => s.AddressLine1).NotEmpty().MaximumLength(255);
                s.RuleFor(s => s.AddressLine2).MaximumLength(255);
                s.RuleFor(s => s.TownCity).NotEmpty().MaximumLength(100);
                s.RuleFor(s => s.County).NotEmpty().MaximumLength(100);
                s.RuleFor(s => s.Country).MaximumLength(100);
                s.RuleFor(s => s.PostalCode).NotEmpty().MaximumLength(16);
                s.RuleFor(s => s.DeliveryNote).MaximumLength(500);
            });
    }
}


