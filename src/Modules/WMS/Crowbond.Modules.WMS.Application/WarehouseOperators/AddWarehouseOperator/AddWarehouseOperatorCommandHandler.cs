using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.AddWarehouseOperator;

internal sealed class AddWarehouseOperatorCommandHandler(
    IWarehouseOperatorRepository warehouseOperatorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddWarehouseOperatorCommand>
{
    public async Task<Result> Handle(AddWarehouseOperatorCommand request, CancellationToken cancellationToken)
    {
        WarehouseOperator? warehouseOperator = await warehouseOperatorRepository.GetAsync(request.UserId, cancellationToken);

        if (warehouseOperator == null)
        {
            warehouseOperator = WarehouseOperator.Create(request.UserId);
            warehouseOperatorRepository.Insert(warehouseOperator);
        }

        warehouseOperator.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
