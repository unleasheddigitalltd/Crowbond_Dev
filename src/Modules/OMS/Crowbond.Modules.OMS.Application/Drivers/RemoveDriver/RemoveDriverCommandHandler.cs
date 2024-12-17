using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;

namespace Crowbond.Modules.OMS.Application.Drivers.RemoveDriver;

internal sealed class RemoveDriverCommandHandler(
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveDriverCommand>
{
    public async Task<Result> Handle(RemoveDriverCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.UserId, cancellationToken);

        if (driver == null)
        {
            return Result.Failure(DriverErrors.NotFound(request.UserId));            
        }

        driver.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
