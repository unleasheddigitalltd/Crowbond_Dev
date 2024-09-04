using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;

internal sealed class CreateWarehouseOperatorCommandHandler(
    IWarehouseOperatorRepository operatorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWarehouseOperatorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWarehouseOperatorCommand request, CancellationToken cancellationToken)
    {
        Result<WarehouseOperator> result = WarehouseOperator.Create(
        request.Operator.FirstName,
        request.Operator.LastName,
        request.Operator.Username,
        request.Operator.Email,
        request.Operator.Mobile);

        operatorRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
