using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProduct;

internal sealed class DeleteCustomerProductCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCustomerProductCommand>
{
    public async Task<Result> Handle(DeleteCustomerProductCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        // Check for existing product
        CustomerProduct? existingProduct = await customerProductRepository
            .GetByCustomerAndProductAsync(request.CustomerId, request.ProductId, cancellationToken);

        if (existingProduct is null || !existingProduct.IsActive)
        {
            return Result.Failure(CustomerProductErrors.NotFound(request.ProductId));
        }

        CustomerProductPriceHistory priceHistory = existingProduct.Deactivate(dateTimeProvider.UtcNow);
        customerProductRepository.InsertPriceHistory(priceHistory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
