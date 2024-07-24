using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Drivers.CreateDriver;

internal sealed class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator()
    {
        RuleFor(d => d.Driver.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(d => d.Driver.LastName).NotEmpty().MaximumLength(100);
        RuleFor(d => d.Driver.Username).NotEmpty().MaximumLength(128);
        RuleFor(d => d.Driver.Email).NotEmpty().MaximumLength(255);
        RuleFor(d => d.Driver.Mobile).NotEmpty().MaximumLength(20);
        RuleFor(d => d.Driver.VehicleRegn).MaximumLength(10);
    }
}
