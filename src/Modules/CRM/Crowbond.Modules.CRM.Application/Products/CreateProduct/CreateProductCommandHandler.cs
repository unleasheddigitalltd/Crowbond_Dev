using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.Products.CreateProduct;
internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand>
{
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = Product.Create(
             request.ProductId,
             request.Name,
             request.Sku,
             request.FilterTypeName,
             request.UnitOfMeasureName,
             request.InventoryTypeName,
             request.CategoryId,
             request.BrandId,
             request.ProductGroupId,
             request.TaxRateType,
             request.IsActive);

        productRepository.Insert(result);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
