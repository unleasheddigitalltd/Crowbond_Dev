using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockLocation;

internal sealed class UpdateStockLocationCommandValidator : AbstractValidator<UpdateStockLocationCommand>
{
    public UpdateStockLocationCommandValidator()
    {
        RuleFor(s => s.StockId).NotEmpty();
        RuleFor(s => s.TransactionNote).MaximumLength(255);
        RuleFor(s => s.ReasonId).NotEmpty();
        RuleFor(s => s.Quantity).GreaterThan(0);
        RuleFor(s => s.Destination).NotEmpty();
    }
}
