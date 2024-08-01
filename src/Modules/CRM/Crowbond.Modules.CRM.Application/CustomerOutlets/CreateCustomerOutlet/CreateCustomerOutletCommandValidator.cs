using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.CreateCustomerOutlet;

internal sealed class CreateCustomerOutletCommandValidator : AbstractValidator<CreateCustomerOutletCommand>
{
    public CreateCustomerOutletCommandValidator()
    {
        RuleFor(c => c.CustomerOutlet.LocationName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.CustomerOutlet.FullName).MaximumLength(100);
        RuleFor(c => c.CustomerOutlet.Email).MaximumLength(255);
        RuleFor(c => c.CustomerOutlet.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(c => c.CustomerOutlet.Mobile).MaximumLength(20);
        RuleFor(c => c.CustomerOutlet.AddressLine1).NotEmpty().MaximumLength(255);
        RuleFor(c => c.CustomerOutlet.AddressLine2).MaximumLength(255);
        RuleFor(c => c.CustomerOutlet.TownCity).NotEmpty().MaximumLength(100);
        RuleFor(c => c.CustomerOutlet.County).NotEmpty().MaximumLength(100);
        RuleFor(c => c.CustomerOutlet.Country).MaximumLength(100);
        RuleFor(c => c.CustomerOutlet.PostalCode).NotEmpty().MaximumLength(20);
        RuleFor(c => c.CustomerOutlet.DeliveryNote).MaximumLength(500);
        RuleFor(c => c.CustomerOutlet.DeliveryTimeFrom).NotEmpty();
        RuleFor(c => c.CustomerOutlet.DeliveryTimeTo).NotEmpty();
        RuleFor(c => c.CustomerOutlet.Is24HrsDelivery).NotEmpty();
    }
}
