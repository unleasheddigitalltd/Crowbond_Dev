using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockStatus;
internal sealed class UpdateStockStatusCommandValidator : AbstractValidator<UpdateStockStatusCommand>
{
    public UpdateStockStatusCommandValidator()
    {
        RuleFor(c => c.StockId).NotEmpty();
        RuleFor(c => c.StatusType).NotEmpty();
    }    
}
