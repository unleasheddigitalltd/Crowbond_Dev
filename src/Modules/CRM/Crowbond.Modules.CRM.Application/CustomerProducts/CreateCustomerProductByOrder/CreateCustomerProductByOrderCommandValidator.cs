using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductByOrder;

internal sealed class CreateCustomerProductByOrderCommandValidator: AbstractValidator<CreateCustomerProductByOrderCommand>
{
    public CreateCustomerProductByOrderCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
    }
}
