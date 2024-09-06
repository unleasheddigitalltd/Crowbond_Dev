using FluentValidation;

namespace Crowbond.Modules.CRM.Application.Customers.DeleteCustomerOutlet;

internal sealed class DeleteCustomerOutletCommandValidator : AbstractValidator<DeleteCustomerOutletCommand>
{
    public DeleteCustomerOutletCommandValidator()
    {
        RuleFor(o => o.CustomerOutletId).NotEmpty();
    }
}

