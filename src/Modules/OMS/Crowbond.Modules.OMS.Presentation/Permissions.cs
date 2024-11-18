namespace Crowbond.Modules.OMS.Presentation;

internal static class Permissions
{    
    internal const string GetPurchaseOrders = "purchase-orders:read";
    internal const string ModifyPurchaseOrders = "purchase-orders:update";
    internal const string CreatePurchaseOrders = "purchase-orders:create";

    internal const string ApprovePurchaseOrders = "purchase-orders:approve";
    internal const string CancelPurchaseOrders = "purchase-orders:cancel";

    internal const string GetRoutes = "routes:read";
    internal const string ModifyRoutes = "routes:update";
    internal const string CreateRoutes = "routes:create";
    
    internal const string GetDrivers = "drivers:read";
    internal const string ModifyDrivers = "drivers:update";
    internal const string CreateDrivers = "drivers:create";

    internal const string GetVehicles = "vehicles:read";
    internal const string ModifyVehicles = "vehicles:update";
    internal const string CreateVehicles = "vehicles:create";

    internal const string GetRouteTrips = "route-trip:read";
    internal const string ModifyRouteTrips = "route-trip:update";
    internal const string CreateRouteTrips = "route-trip:create";

    internal const string GetCompliances = "compliances:read";
    internal const string ModifyOtherRouteTripLogs = "route-trip-log:update:other";

    internal const string GetCart = "carts:read";
    internal const string AddToCart = "carts:add";
    internal const string RemoveFromCart = "carts:remove";

    internal const string GetOrders = "orders:read";
    internal const string GetMyOrders = "orders:read:my";
    internal const string CreateOrders = "orders:create";
    internal const string CreateMyOrders = "orders:create:my";
    internal const string DeleteOrders = "orders:delete";
    internal const string AcceptOrders = "orders:accept";
    internal const string DeliverOrders = "orders:deliver";

    internal const string ReviewOrderLine = "order-lines:review";
}
