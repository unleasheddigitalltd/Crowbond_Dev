using Crowbond.Common.Domain;

namespace Crowbond.Modules.Products.Domain.Products;
public static class ProductErrors
{
    public static Error NotFound(Guid productId) =>
    Error.NotFound("Products.NotFound", $"The event with the identifier {productId} was not found");

    public static readonly Error AlreadyHeld = Error.Problem(
        "Product.AlreadyHeld",
        "The product was already held");

    public static readonly Error AlreadyActived = Error.Problem(
        "Product.AlreadyActived",
        "The product was already actived");
}
