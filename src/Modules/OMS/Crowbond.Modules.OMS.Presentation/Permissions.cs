namespace Crowbond.Modules.OMS.Presentation;

internal static class Permissions
{
    internal const string GetOrders = "orders:read";
    
    internal const string GetPurchaseOrders = "purchaseorders:read";
    internal const string ModifyPurchaseOrders = "purchaseorders:update";
    internal const string CreatePurchaseOrders = "purchaseorders:create";
    
    internal const string GetRoutes = "routes:read";
    internal const string ModifyRoutes = "routes:update";
    internal const string CreateRoutes = "routes:create";
    
    internal const string GetDrivers = "drivers:read";
    internal const string ModifyDrivers = "drivers:update";
    internal const string CreateDrivers = "drivers:create";

    internal const string GetRouteTrips = "routetrip:read";
    internal const string ModifyRouteTrips = "routetrip:update";
    internal const string CreateRouteTrips = "routetrip:create";
}
