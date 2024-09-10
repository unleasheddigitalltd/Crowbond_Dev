using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

internal sealed class UpdateCustomerProductCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerProductCommand>
{
    public async Task<Result> Handle(UpdateCustomerProductCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        IEnumerable<CustomerProduct> existingProducts = await customerProductRepository.GetForCustomerAsync(request.CustomerId, cancellationToken);

        var productsToDelete = existingProducts
            .Where(p => !request.CustomerProducts
            .Any(dto => dto.ProductId == p.ProductId && dto.FixedDiscount == p.FixedDiscount && dto.FixedPrice == p.FixedPrice))
            .ToList();

        var productDictionary = existingProducts
            .Where(p => !productsToDelete.Contains(p)) // Keep products not marked for deletion
            .ToDictionary(p => p.ProductId);

        // Identify products to add or update
        foreach (CustomerProductRequest productDto in request.CustomerProducts)
        {
            if (productDictionary.TryGetValue(productDto.ProductId, out CustomerProduct productsNotToDelete))
            {
                // Update existing product
                Result result = productsNotToDelete.Update(
                    productDto.Comments,
                    productDto.EffectiveDate,
                    productDto.ExpiryDate,
                    dateTimeProvider.UtcNow);

                if (result.IsFailure)
                {
                    return Result.Failure(result.Error);
                }
            }
            else
            {
                // Add new product
                Result<CustomerProduct> result = CustomerProduct.Create(
                    request.CustomerId,
                    productDto.ProductId,
                    productDto.FixedPrice,
                    productDto.FixedDiscount,
                    productDto.Comments,
                    productDto.EffectiveDate,
                    productDto.ExpiryDate,
                    dateTimeProvider.UtcNow);

                if (result.IsFailure)
                {
                    return Result.Failure(result.Error);
                }

                customerProductRepository.Insert(result.Value);
            }
        }

        // Delete products not in the updated list
        foreach (CustomerProduct product in productsToDelete)
        {
            customerProductRepository.Remove(product);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
