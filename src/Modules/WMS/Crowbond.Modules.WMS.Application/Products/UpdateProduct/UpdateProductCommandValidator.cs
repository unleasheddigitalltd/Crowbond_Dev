using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Product.Sku).NotEmpty();
        RuleFor(p => p.Product.Name).NotEmpty();
        RuleFor(p => p.Product.FilterTypeName).NotEmpty();
        RuleFor(p => p.Product.UnitOfMeasureName).NotEmpty();
        RuleFor(p => p.Product.CategoryId).NotEmpty();
        RuleFor(p => p.Product.InventoryTypeName).NotEmpty();
    }
}
