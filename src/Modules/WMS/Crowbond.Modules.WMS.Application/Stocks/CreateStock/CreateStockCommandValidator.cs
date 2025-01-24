using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Stocks.CreateStock;
internal sealed class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
{
    public CreateStockCommandValidator()
    {
        RuleFor(s => s.ReceiptLineId).NotEmpty();
        RuleFor(s => s.LocationId).NotEmpty();
        RuleFor(s => s.Qty).GreaterThan(decimal.Zero);
    }
}
