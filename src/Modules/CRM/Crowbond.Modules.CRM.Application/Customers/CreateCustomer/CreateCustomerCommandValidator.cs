﻿using FluentValidation;

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
    }
}


