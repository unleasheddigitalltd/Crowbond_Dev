using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Vehicles.UpdateVehicle;

internal sealed class UpdateVehicleCommandValidator: AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(v => v.VehicleId).NotEmpty();
        RuleFor(v => v.VehicleRegn).NotEmpty().MaximumLength(10);
    }
}
