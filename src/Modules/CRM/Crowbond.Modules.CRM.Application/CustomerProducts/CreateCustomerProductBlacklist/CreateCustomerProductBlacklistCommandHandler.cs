using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductBlacklist;

internal sealed class CreateCustomerProductBlacklistCommandHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerProductBlacklistCommand>
{
    public async Task<Result> Handle(CreateCustomerProductBlacklistCommand request, CancellationToken cancellationToken)
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
        CustomerProductBlacklist? existingProduct = await customerProductRepository
        .GetBlacklistByCustomerAndProductAsync(request.CustomerId, request.ProductId, cancellationToken);

        if (existingProduct is not null)
        {
            return Result.Failure(CustomerProductErrors.Exists(existingProduct.Id));
        }

        // Create new product if not existing
        Result<CustomerProductBlacklist> createResult = CustomerProductBlacklist.Create(
            request.CustomerId,
            request.ProductId,
            request.Comments);

        if (createResult.IsFailure)
        {
            return Result.Failure(createResult.Error);
        }

        customerProductRepository.InsertBlacklist(createResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
