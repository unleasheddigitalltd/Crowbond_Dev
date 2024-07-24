using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;

namespace Crowbond.Modules.OMS.Application.Drivers.CreateDriver;

internal sealed class CreateDriverCommandHandler(
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateDriverCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        Result<Driver> result = Driver.Create(
        request.Driver.FirstName,
        request.Driver.LastName,
        request.Driver.Username,
        request.Driver.Email,
        request.Driver.Mobile,
        request.Driver.VehicleRegn);

        driverRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }        

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
