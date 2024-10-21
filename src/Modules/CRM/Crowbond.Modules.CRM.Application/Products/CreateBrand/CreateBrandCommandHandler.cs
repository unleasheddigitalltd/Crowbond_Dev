using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.Products.CreateBrand;

internal sealed class CreateBrandCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBrandCommand>
{
    public async Task<Result> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(request.Id, request.Name);

        productRepository.InsertBrand(brand);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
