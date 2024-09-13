using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.ProductPrices;

namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdatePriceTierPrices;

internal sealed class UpdatePriceTierPricesCommandHandler(
    IPriceTierRepository priceTierRepository,
    IProductPriceRepository productPriceRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePriceTierPricesCommand>
{
    public async Task<Result> Handle(UpdatePriceTierPricesCommand request, CancellationToken cancellationToken)
    {
        PriceTier? priceTier = await priceTierRepository.GetAsync(request.PriceTierId, cancellationToken);
        if (priceTier is null)
        {
            return Result.Failure(PriceTierErrors.NotFound(request.PriceTierId));
        }

        IEnumerable<ProductPrice> existingProductPrices = await productPriceRepository.GetForPriceTierAsync(request.PriceTierId, cancellationToken);

        var productDictionary = existingProductPrices.ToDictionary(p => p.ProductId);

        foreach (ProductPriceRequest dto in request.ProductPrices)
        {
            if (productDictionary.TryGetValue(dto.ProductId, out ProductPrice existingProductPrice))
            {
                if (existingProductPrice.SalePrice != dto.SalePrice || existingProductPrice.BasePurchasePrice != dto.BasePurchasePrice)
                {
                    // Replace the changed existing product
                    productPriceRepository.Remove(existingProductPrice);
                    var changedProductPrice = ProductPrice.Create(
                        dto.ProductId,
                        request.PriceTierId,
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
                    dto.ProductId,
                    request.PriceTierId,
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
