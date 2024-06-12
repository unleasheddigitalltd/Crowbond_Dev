using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Product.Sku).NotEmpty();
        RuleFor(p => p.Product.Name).NotEmpty();
        RuleFor(p => p.Product.FilterType).NotEmpty();
        RuleFor(p => p.Product.UnitOfMeasure).NotEmpty();
        RuleFor(p => p.Product.Category).NotEmpty();
        RuleFor(p => p.Product.InventoryType).NotEmpty();
    }
}
