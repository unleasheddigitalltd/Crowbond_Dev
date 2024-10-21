﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Products.UpdateBrand;

internal sealed class UpdateBrandCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateBrandCommand>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await productRepository.GetBrandAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure(ProductErrors.BrandNotFound(request.Id));
        }

        brand.Update(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
