using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.Products.CreateProductGroup;

internal sealed class CreateProductGroupCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductGroupCommand>
{
    public async Task<Result> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = ProductGroup.Create(request.Id, request.Name);

        productRepository.InsertProductGroup(productGroup);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
