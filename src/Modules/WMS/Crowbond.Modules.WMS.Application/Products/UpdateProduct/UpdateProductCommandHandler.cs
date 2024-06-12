using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Application.Products.UpdateProduct.Dtos;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductDto>(ProductErrors.NotFound(request.Id));
        }

        product.Update(
            sku: request.Product.Sku,
            name: request.Product.Name,
            parentId: request.Product.Parent,
            filterTypeName: request.Product.FilterType,
            unitOfMeasureName: request.Product.UnitOfMeasure,
            categoryId: request.Product.Category,
            inventoryTypeName: request.Product.InventoryType,
            barcode: request.Product.Barcode,
            packSize: request.Product.PackSize,
            handlingNotes: request.Product.HandlingNotes,
            qiCheck: request.Product.QiCheck,
            notes: request.Product.Notes,
            reorderLevel: request.Product.ReorderLevel,
            height: request.Product.Height,
            width: request.Product.Width,
            length: request.Product.Length,
            weightInput: request.Product.WeightInput,
            active: request.Product.Active
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);
        var result = new ProductDto(
            request.Id,
            request.Product.Sku,
            request.Product.Name,
            request.Product.Parent,
            request.Product.FilterType,
            request.Product.UnitOfMeasure,
            request.Product.Category,
            request.Product.InventoryType,
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
            request.Product.Active);

        return Result.Success(result);
    }
}
