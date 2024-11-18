using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProduct;

internal sealed class CreateCustomerProductCommandHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerProductCommand>
{
    public async Task<Result> Handle(CreateCustomerProductCommand request, CancellationToken cancellationToken)
    {
        // Check if customer exists
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        // Check if product exists
        Product? product = await productRepository.GetAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(request.ProductId));
        }

        // Check for existing product for this customer
        CustomerProduct? existingProduct = await customerProductRepository
        .GetByCustomerAndProductAsync(request.CustomerId, request.ProductId, cancellationToken);

        if (existingProduct is not null)
        {
            // Prevent duplicate active product
            if (existingProduct.IsActive)
            {
                return Result.Failure(CustomerProductErrors.Exists(existingProduct.Id));
            }

            // Update inactive product
            Result<CustomerProductPriceHistory> updateResult = existingProduct.Update(
                request.FixedPrice,
                request.FixedDiscount,
                request.Comments,
                request.EffectiveDate,
                request.ExpiryDate,
                dateTimeProvider.UtcNow);

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            customerProductRepository.InsertPriceHistory(updateResult.Value);
        }
        else
        {
            // Create new product if not existing
            Result<CustomerProduct> createResult = CustomerProduct.Create(
                request.CustomerId,
                request.ProductId,
                request.FixedPrice,
                request.FixedDiscount,
                request.Comments,
                request.EffectiveDate,
                request.ExpiryDate,
                dateTimeProvider.UtcNow);

            if (createResult.IsFailure)
            {
                return Result.Failure(createResult.Error);
            }

            customerProductRepository.Insert(createResult.Value);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
