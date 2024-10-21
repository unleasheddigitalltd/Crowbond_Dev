using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Products.UpdateBrand;

internal sealed class UpdateBrandCommandValidator: AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.Name).NotEmpty().MaximumLength(100);
    }
}
