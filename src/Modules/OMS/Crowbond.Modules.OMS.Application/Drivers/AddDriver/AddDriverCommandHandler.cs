using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;

namespace Crowbond.Modules.OMS.Application.Drivers.AddDriver;

internal sealed class AddDriverCommandHandler(
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddDriverCommand>
{
    public async Task<Result> Handle(AddDriverCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.UserId, cancellationToken);

        if (driver == null)
        {

            driver = Driver.Create(request.UserId);

            driverRepository.Insert(driver);

        }

        driver.Activate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
