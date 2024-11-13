using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProduct;

internal sealed class DeleteCustomerProductCommandValidator: AbstractValidator<DeleteCustomerProductCommand>
{
    public DeleteCustomerProductCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
    }
}
