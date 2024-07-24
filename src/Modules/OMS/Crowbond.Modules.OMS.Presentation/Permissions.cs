namespace Crowbond.Modules.OMS.Presentation;

internal static class Permissions
{
    internal const string GetOrders = "orders:read";
    
    internal const string GetPurchaseOrders = "purchaseorders:read";
    internal const string ModifyPurchaseOrders = "purchaseorders:update";
    internal const string CreatePurchaseOrders = "purchaseorders:create";
    
    internal const string GetRoutes = "routes:read";
    
    internal const string GetDrivers = "drivers:read";
    internal const string ModifyDrivers = "drivers:update";
    internal const string CreateDrivers = "drivers:create";
}
