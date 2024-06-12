using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Product.Sku).NotEmpty();
        RuleFor(p => p.Product.Name).NotEmpty();
        RuleFor(p => p.Product.FilterType).NotEmpty();
        RuleFor(p => p.Product.UnitOfMeasure).NotEmpty();
        RuleFor(p => p.Product.Category).NotEmpty();
        RuleFor(p => p.Product.InventoryType).NotEmpty();
        RuleFor(p => p.Product.HandlingNotes).MaximumLength(500);
        RuleFor(p => p.Product.Notes).MaximumLength(500);
    }
}
