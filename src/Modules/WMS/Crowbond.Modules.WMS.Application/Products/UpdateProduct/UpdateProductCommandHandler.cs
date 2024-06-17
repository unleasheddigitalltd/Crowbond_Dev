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
        Product? product = await productRepository.GetAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.Id));
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

        return Result.Success();
    }
}
