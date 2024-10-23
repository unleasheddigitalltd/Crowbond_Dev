using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Vehicles;

namespace Crowbond.Modules.OMS.Application.Vehicles.UpdateVehicle;

internal sealed class UpdateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateVehicleCommand>
{
    public async Task<Result> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await vehicleRepository.GetAsync(request.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure(VehicleErrors.NotFound(request.VehicleId));
        }

        vehicle.Update(request.VehicleRegn);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
