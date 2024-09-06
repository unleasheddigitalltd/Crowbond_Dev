using System.Xml.Linq;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Categories;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(request.Product.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(request.Product.CategoryId));
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
             filterType,
             unitOfMeasure,
             category,
             inventoryType,
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
