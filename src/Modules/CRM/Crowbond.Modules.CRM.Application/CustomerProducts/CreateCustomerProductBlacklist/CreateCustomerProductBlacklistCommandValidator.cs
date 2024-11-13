using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductBlacklist;

internal sealed class CreateCustomerProductBlacklistCommandValidator: AbstractValidator<CreateCustomerProductBlacklistCommand>
{
    public CreateCustomerProductBlacklistCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
        RuleFor(cp => cp.Comments).MaximumLength(255);
    }
}
