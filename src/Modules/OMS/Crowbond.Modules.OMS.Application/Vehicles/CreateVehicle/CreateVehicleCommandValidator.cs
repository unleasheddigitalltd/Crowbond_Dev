using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Vehicles.CreateVehicle;

internal sealed class CreateVehicleCommandValidator: AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(v => v.VehicleRegn).NotEmpty().MaximumLength(10);
    }
}
