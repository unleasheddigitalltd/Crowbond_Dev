using FluentValidation;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;

internal sealed class CreateWarehouseOperatorCommandValidator : AbstractValidator<CreateWarehouseOperatorCommand>
{
    public CreateWarehouseOperatorCommandValidator()
    {
        RuleFor(d => d.Operator.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(d => d.Operator.LastName).NotEmpty().MaximumLength(100);
        RuleFor(d => d.Operator.Username).NotEmpty().MaximumLength(128);
        RuleFor(d => d.Operator.Email).NotEmpty().MaximumLength(255);
        RuleFor(d => d.Operator.Mobile).NotEmpty().MaximumLength(20).Matches(@"^(\+44|0)7\d{9}$");
    }
}
