using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.UpdateSupplierProducts;

internal sealed class UpdateSupplierProductsCommandHandler(
    ISupplierRepository supplierRepository,
    ISupplierProductRepository supplierProductRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierProductsCommand>
{
    public async Task<Result> Handle(UpdateSupplierProductsCommand request, CancellationToken cancellationToken)
    {
        Supplier? supplier = await supplierRepository.GetAsync(request.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound(request.SupplierId));
        }

        IEnumerable<SupplierProduct> existingProducts = await supplierProductRepository.GetForSupplierAsync(request.SupplierId, cancellationToken);

        var productsToDelete = existingProducts
            .Where(p => !request.SupplierProducts
            .Any(dto => dto.ProductId == p.ProductId && dto.UnitPrice == p.UnitPrice))
            .ToList();

        var productDictionary = existingProducts
            .Where(p => !productsToDelete.Contains(p)) // Keep products not marked for deletion
            .ToDictionary(p => p.ProductId);

        // Identify products to add or update
        foreach (SupplierProductRequest productDto in request.SupplierProducts)
        {
            if (productDictionary.TryGetValue(productDto.ProductId, out SupplierProduct productsNotToDelete))
            {
                // Update existing product
                productsNotToDelete.Update(productDto.IsDefault, productDto.Comments, request.UserId, dateTimeProvider.UtcNow);
            }
            else
            {
                // Add new product
                var newProduct = SupplierProduct.Create(
                    request.SupplierId,
                    productDto.ProductId,
                    productDto.UnitPrice,
                    productDto.IsDefault,
                    productDto.Comments, 
                    request.UserId, 
                    dateTimeProvider.UtcNow);
                supplierProductRepository.Insert(newProduct);
            }
        }

        // Delete products not in the updated list
        foreach (SupplierProduct product in productsToDelete)
        {
            supplierProductRepository.Remove(product);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
