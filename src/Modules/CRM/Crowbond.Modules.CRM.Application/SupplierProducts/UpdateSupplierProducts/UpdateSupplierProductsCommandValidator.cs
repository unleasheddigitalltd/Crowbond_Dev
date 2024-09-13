using FluentValidation;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.UpdateSupplierProducts;

internal sealed class UpdateSupplierProductsCommandValidator : AbstractValidator<UpdateSupplierProductsCommand>
{
    public UpdateSupplierProductsCommandValidator()
    {
        RuleFor(s => s.SupplierId).NotEmpty();

        RuleForEach(s => s.SupplierProducts).ChildRules(products =>
        {
            products.RuleFor(p => p.ProductId).NotEmpty();
            products.RuleFor(p => p.UnitPrice).GreaterThan(0);
            products.RuleFor(p => p.IsDefault).NotNull();
            products.RuleFor(p => p.Comments).MaximumLength(255);
        });

        RuleFor(p => p.SupplierProducts).Must(coll => coll.GroupBy(p => p.ProductId).Count() == coll.Count)
            .WithMessage(coll => $"One or more items have duplicate products");
    }
}
