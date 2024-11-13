using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProductBlacklist;

internal sealed class UpdateCustomerProductBlacklistCommandHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerProductBlacklistCommand>
{
    public async Task<Result> Handle(UpdateCustomerProductBlacklistCommand request, CancellationToken cancellationToken)
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

        if (existingProduct is null)
        {
            return Result.Failure(CustomerProductErrors.BlacklistNotFound(request.ProductId));
        }

        // Update product
        existingProduct.Update(request.Comments);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
