using System.Xml.Linq;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Application.Products.CreateProduct.Dtos;
using Crowbond.Modules.WMS.Domain.Categories;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(request.Product.Category, cancellationToken);

        if (category is null)
        {
            return Result.Failure<ProductResponse>(CategoryErrors.NotFound(request.Product.Category));
        }

        FilterType? filterType = await productRepository.GetFilterTypeAsync(request.Product.FilterType, cancellationToken);

        if (filterType is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.FilterTypeNotFound(request.Product.FilterType));
        }

        InventoryType? inventoryType = await productRepository.GetInventoryTypeAsync(request.Product.InventoryType, cancellationToken);

        if (inventoryType is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.InventoryTypeNotFound(request.Product.InventoryType));
        }

        UnitOfMeasure? unitOfMeasure = await productRepository.GetUnitOfMeasureAsync(request.Product.UnitOfMeasure, cancellationToken);

        if (unitOfMeasure is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.UnitOfMeasureNotFound(request.Product.UnitOfMeasure));
        }

        Result<Product> result = Product.Create(
             request.Product.Sku,
             request.Product.Name,
             request.Product.Parent,
             filterType,
             unitOfMeasure,
             category,
             inventoryType,
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
            return Result.Failure<ProductResponse>(result.Error);
        }

        productRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ProductResponse(
                result.Value.Id,
                result.Value.Sku,
                result.Value.Name,
                result.Value.ParentId,
                result.Value.FilterTypeName,
                result.Value.UnitOfMeasureName,
                result.Value.CategoryId,
                result.Value.InventoryTypeName,
                result.Value.Barcode,
                result.Value.PackSize,
                result.Value.HandlingNotes,
                result.Value.QiCheck,
                result.Value.Notes,
                result.Value.ReorderLevel,
                result.Value.Height,
                result.Value.Width,
                result.Value.Length,
                result.Value.WeightInput,
                result.Value.Active
            );

        return response;
    }
}
