using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.CreateCategory;
internal sealed class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
    }
}
