using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.RemoveWarehouseOperator;

internal sealed class RemoveWarehouseOperatorCommandHandler(
    IWarehouseOperatorRepository warehouseOperatorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveWarehouseOperatorCommand>
{
    public async Task<Result> Handle(RemoveWarehouseOperatorCommand request, CancellationToken cancellationToken)
    {
        WarehouseOperator? warehouseOperator = await warehouseOperatorRepository.GetAsync(request.UserId, cancellationToken);

        if (warehouseOperator == null)
        {
            return Result.Failure(WarehouseOperatorErrors.NotFound(request.UserId));
        }

        warehouseOperator.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
