using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.DeleteCustomerProductBlacklist;

internal sealed class DeleteCustomerProductBlacklistCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerProductRepository customerProductRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCustomerProductBlacklistCommand>
{
    public async Task<Result> Handle(DeleteCustomerProductBlacklistCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        // Check for existing product
        CustomerProductBlacklist? existingProduct = await customerProductRepository
            .GetBlacklistByCustomerAndProductAsync(request.CustomerId, request.ProductId, cancellationToken);

        if (existingProduct is null)
        {
            return Result.Failure(CustomerProductErrors.BlacklistNotFound(request.ProductId));
        }

        customerProductRepository.RemoveBlacklist(existingProduct);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
