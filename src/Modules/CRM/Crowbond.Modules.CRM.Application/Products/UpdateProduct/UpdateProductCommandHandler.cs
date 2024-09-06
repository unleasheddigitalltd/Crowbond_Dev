using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        product.Update(
            request.Name, 
            request.Sku, 
            request.FilterTypeName, 
            request.UnitOfMeasureName, 
            request.InventoryTypeName, 
            request.CategoryId, 
            request.CategoryName,
            request.TaxRateType,
            request.IsActive);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
