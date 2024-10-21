using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.CreateProductGroup;

internal sealed class CreateProductGroupCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductGroupCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = ProductGroup.Create(request.Name);

        productRepository.InsertProductGroup(productGroup);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(productGroup.Id);
    }
}
