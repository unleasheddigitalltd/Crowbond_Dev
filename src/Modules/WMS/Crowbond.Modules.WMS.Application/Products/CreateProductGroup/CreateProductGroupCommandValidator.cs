using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.CreateProductGroup;
internal sealed class CreateProductGroupCommandValidator : AbstractValidator<CreateProductGroupCommand>
{
    public CreateProductGroupCommandValidator()
    {
        RuleFor(pg => pg.Name).NotEmpty().MaximumLength(100);
    }
}
