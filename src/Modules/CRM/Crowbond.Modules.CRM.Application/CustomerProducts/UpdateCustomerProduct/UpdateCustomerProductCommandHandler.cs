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

        // Identify products that need to be deactivated
        var productsToDeactivate = existingProducts
            .Where(p => !request.CustomerProducts
            .Any(dto => dto.ProductId == p.ProductId))
            .ToList();

        // Group existing products by their ProductId for quick lookup
        var existingProductMap = existingProducts
            .Where(p => !productsToDeactivate.Contains(p)) // Keep products not marked for deletion
            .ToDictionary(p => p.ProductId);

        // Process new or updated customer products
        foreach (CustomerProductRequest productDto in request.CustomerProducts)
        {
            if (existingProductMap.TryGetValue(productDto.ProductId, out CustomerProduct existingProduct))
            {
                // Update or add price to an existing product
                Result result = existingProduct.Update(
                    productDto.FixedPrice,
                    productDto.FixedDiscount,
                    productDto.Comments,
                    productDto.EffectiveDate,
                    productDto.ExpiryDate,
                    dateTimeProvider.UtcNow);

                if (result.IsFailure)
                {
                    return result;
                }
            }
            else
            {
                // Add a new product
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
                    return result;
                }

                customerProductRepository.Insert(result.Value);
            }
        }

        // Deactivate products that are no longer in the request
        foreach (CustomerProduct product in productsToDeactivate)
        {
            product.Deactivate();
        }

        // Commit all changes to the database
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
