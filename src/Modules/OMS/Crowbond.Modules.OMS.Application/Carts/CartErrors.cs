using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Application.Carts;

public static class CartErrors
{
    public static readonly Error Empty = Error.Problem("Carts.Empty", "The cart is empty");
}
