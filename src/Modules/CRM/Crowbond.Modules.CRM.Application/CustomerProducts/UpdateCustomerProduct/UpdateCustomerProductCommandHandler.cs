using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

internal sealed class UpdateCustomerProductCommandHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerProductCommand>
{
    public async Task<Result> Handle(UpdateCustomerProductCommand request, CancellationToken cancellationToken)
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

        if (existingProduct is null || !existingProduct.IsActive)
        {
            return Result.Failure(CustomerProductErrors.NotFound(request.ProductId));
        }

        // Update product
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

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
