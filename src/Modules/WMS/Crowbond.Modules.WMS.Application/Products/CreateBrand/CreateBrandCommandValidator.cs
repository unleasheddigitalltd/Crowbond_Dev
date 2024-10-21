using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.CreateBrand;

internal sealed class CreateBrandCommandValidator: AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name).NotEmpty().MaximumLength(100);
    }
}
