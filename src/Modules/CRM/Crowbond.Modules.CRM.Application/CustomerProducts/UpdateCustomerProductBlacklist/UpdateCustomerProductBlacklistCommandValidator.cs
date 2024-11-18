using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProductBlacklist;

internal sealed class UpdateCustomerProductBlacklistCommandValidator: AbstractValidator<UpdateCustomerProductBlacklistCommand>
{
    public UpdateCustomerProductBlacklistCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
        RuleFor(cp => cp.Comments).MaximumLength(255);
    }
}
