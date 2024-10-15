using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProductGroup;

internal sealed class UpdateProductGroupCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductGroupCommand>
{
    public async Task<Result> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
    {
        ProductGroup? productGroup = await productRepository.GetProductGroupAsync(request.Id, cancellationToken);

        if (productGroup is null)
        {
            return Result.Failure(ProductErrors.ProductGroupNotFound(request.Id));
        }

        productGroup.Update(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
