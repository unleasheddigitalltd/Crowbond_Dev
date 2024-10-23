using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Vehicles;

namespace Crowbond.Modules.OMS.Application.Vehicles.CreateVehicle;

internal sealed class CreateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateVehicleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = Vehicle.Create(request.VehicleRegn);
        vehicleRepository.Insert(vehicle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(vehicle.Id);
    }
}
