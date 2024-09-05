using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Product.Sku).NotEmpty();
        RuleFor(p => p.Product.Name).NotEmpty();
        RuleFor(p => p.Product.FilterTypeName).NotEmpty();
        RuleFor(p => p.Product.UnitOfMeasureName).NotEmpty();
        RuleFor(p => p.Product.CategoryId).NotEmpty();
        RuleFor(p => p.Product.InventoryTypeName).NotEmpty();
        RuleFor(p => p.Product.HandlingNotes).MaximumLength(500);
        RuleFor(p => p.Product.Notes).MaximumLength(500);
    }
}
