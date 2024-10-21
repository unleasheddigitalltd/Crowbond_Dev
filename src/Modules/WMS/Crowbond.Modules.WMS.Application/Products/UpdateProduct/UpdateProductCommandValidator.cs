using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Product.Sku).NotEmpty().MaximumLength(20);
        RuleFor(p => p.Product.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Product.FilterTypeName).NotEmpty().MaximumLength(20);
        RuleFor(p => p.Product.UnitOfMeasureName).NotEmpty().MaximumLength(20);
        RuleFor(p => p.Product.CategoryId).NotEmpty();
        RuleFor(p => p.Product.BrandId).NotEmpty();
        RuleFor(p => p.Product.ProductGroupId).NotEmpty();
        RuleFor(p => p.Product.InventoryTypeName).NotEmpty().MaximumLength(20);
        RuleFor(p => p.Product.HandlingNotes).MaximumLength(500);
        RuleFor(p => p.Product.Notes).MaximumLength(500);
    }
}
