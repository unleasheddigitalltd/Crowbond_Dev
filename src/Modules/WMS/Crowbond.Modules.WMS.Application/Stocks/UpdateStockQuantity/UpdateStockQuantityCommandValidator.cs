using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockQuantity;

internal sealed class UpdateStockQuantityCommandValidator : AbstractValidator<UpdateStockQuantityCommand>
{
    public UpdateStockQuantityCommandValidator()
    {
        RuleFor(c => c.Quantity).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.TransactionNote).MaximumLength(255);
        RuleFor(c => c.ReasonId).NotEmpty();
    }
}
