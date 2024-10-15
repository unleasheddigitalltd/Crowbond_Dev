using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Category? category = await productRepository.GetCategoryAsync(request.Product.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(ProductErrors.CategoryNotFound(request.Product.CategoryId));
        }

        Brand? brand = await productRepository.GetBrandAsync(request.Product.BrandId, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Guid>(ProductErrors.BrandNotFound(request.Product.BrandId));
        }

        ProductGroup? productGroup = await productRepository.GetProductGroupAsync(request.Product.ProductGroupId, cancellationToken);

        if (productGroup is null)
        {
            return Result.Failure<Guid>(ProductErrors.ProductGroupNotFound(request.Product.ProductGroupId));
        }

        Product? product = await productRepository.GetAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.Id));
        }

        product.Update(
            request.Product.Sku,
            request.Product.Name,
            request.Product.Parent,
            request.Product.FilterTypeName,
            request.Product.UnitOfMeasureName,
            request.Product.InventoryTypeName,
            request.Product.CategoryId,
            request.Product.BrandId,
            request.Product.ProductGroupId,
            request.Product.TaxRateType,
            request.Product.Barcode,
            request.Product.PackSize,
            request.Product.HandlingNotes,
            request.Product.QiCheck,
            request.Product.Notes,
            request.Product.ReorderLevel,
            request.Product.Height,
            request.Product.Width,
            request.Product.Length,
            request.Product.WeightInput,
            request.Product.IsActive
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
