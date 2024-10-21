using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProductGroup;

internal sealed class UpdateProductGroupCommandValidator: AbstractValidator<UpdateProductGroupCommand>
{
    public UpdateProductGroupCommandValidator()
    {
        RuleFor(pg => pg.Id).NotEmpty();
        RuleFor(pg => pg.Name).NotEmpty().MaximumLength(100);
    }
}
