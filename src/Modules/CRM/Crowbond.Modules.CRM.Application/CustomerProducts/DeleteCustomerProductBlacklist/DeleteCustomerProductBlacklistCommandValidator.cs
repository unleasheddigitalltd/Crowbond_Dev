using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProductBlacklist;

internal sealed class DeleteCustomerProductBlacklistCommandValidator: AbstractValidator<DeleteCustomerProductBlacklistCommand>
{
    public DeleteCustomerProductBlacklistCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
    }
}
