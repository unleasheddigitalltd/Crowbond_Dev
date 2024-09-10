using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.ProductPrices;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdateProductPrices;

internal sealed class UpdateProductPricesCommandHandler(
    IProductRepository productRepository,
    IProductPriceRepository productPriceRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductPricesCommand>
{
    public async Task<Result> Handle(UpdateProductPricesCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        IEnumerable<ProductPrice> existingProductPrices = await productPriceRepository.GetForProductAsync(request.ProductId, cancellationToken);

        var productDictionary = existingProductPrices.ToDictionary(p => p.PriceTierId);

        foreach (ProductPriceRequest dto in request.ProductPrices)
        {
            if (productDictionary.TryGetValue(dto.PriceTierId, out ProductPrice existingProductPrice))
            {
                if (existingProductPrice.SalePrice != dto.SalePrice || existingProductPrice.BasePurchasePrice != dto.BasePurchasePrice)
                {
                    // Replace the changed existing product
                    productPriceRepository.Remove(existingProductPrice);
                    var changedProductPrice = ProductPrice.Create(
                        request.ProductId,
                        dto.PriceTierId,
                        dto.BasePurchasePrice,
                        dto.SalePrice,
                        DateOnly.FromDateTime(dateTimeProvider.UtcNow)
                    );
                    productPriceRepository.Insert(changedProductPrice);
                }
            }
            else
            {
                // Add new product
                var newProductPrice = ProductPrice.Create(
                    request.ProductId,
                    dto.PriceTierId,
                    dto.BasePurchasePrice,
                    dto.SalePrice,
                    DateOnly.FromDateTime(dateTimeProvider.UtcNow)
                    );
                productPriceRepository.Insert(newProductPrice);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
