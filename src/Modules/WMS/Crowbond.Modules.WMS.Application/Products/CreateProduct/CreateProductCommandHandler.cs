using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.AspNetCore.Http.Features;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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

        FilterType? filterType = await productRepository.GetFilterTypeAsync(request.Product.FilterTypeName, cancellationToken);

        if (filterType is null)
        {
            return Result.Failure<Guid>(ProductErrors.FilterTypeNotFound(request.Product.FilterTypeName));
        }

        InventoryType? inventoryType = await productRepository.GetInventoryTypeAsync(request.Product.InventoryTypeName, cancellationToken);

        if (inventoryType is null)
        {
            return Result.Failure<Guid>(ProductErrors.InventoryTypeNotFound(request.Product.InventoryTypeName));
        }

        UnitOfMeasure? unitOfMeasure = await productRepository.GetUnitOfMeasureAsync(request.Product.UnitOfMeasureName, cancellationToken);

        if (unitOfMeasure is null)
        {
            return Result.Failure<Guid>(ProductErrors.UnitOfMeasureNotFound(request.Product.UnitOfMeasureName));
        }

        Result<Product> result = Product.Create(
             request.Product.Sku,
             request.Product.Name,
             request.Product.Parent,
             request.Product.FilterTypeName,
             request.Product.UnitOfMeasureName,
             request.Product.InventoryTypeName,
             request.Product.CategoryId,
             request.Product.DefaultLocation,
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
             request.Product.WeightInput
            );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        productRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
