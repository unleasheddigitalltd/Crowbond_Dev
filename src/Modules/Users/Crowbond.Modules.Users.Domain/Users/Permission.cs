namespace Crowbond.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");

    public static readonly Permission GetEvents = new("events:read");
    public static readonly Permission SearchEvents = new("events:search");
    public static readonly Permission ModifyEvents = new("events:update");
    public static readonly Permission GetTicketTypes = new("ticket-types:read");
    public static readonly Permission ModifyTicketTypes = new("ticket-types:update");
    public static readonly Permission GetCategories = new("categories:read");
    public static readonly Permission ModifyCategories = new("categories:update");
    public static readonly Permission GetCart = new("carts:read");
    public static readonly Permission AddToCart = new("carts:add");
    public static readonly Permission RemoveFromCart = new("carts:remove");
    public static readonly Permission GetOrders = new("orders:read");
    public static readonly Permission CreateOrder = new("orders:create");
    public static readonly Permission GetTickets = new("tickets:read");
    public static readonly Permission CheckInTicket = new("tickets:check-in");
    public static readonly Permission GetEventStatistics = new("event-statistics:read");

    public static readonly Permission GetProducts = new("products:read");
    public static readonly Permission CreateProducts = new("products:create");
    public static readonly Permission ModifyProducts = new("products:update");

    public static readonly Permission GetStocks = new("stocks:read");
    public static readonly Permission GetStockTransactions = new("stock-transactions:read");
    public static readonly Permission AdjustStocks = new("stocks:adjust");
    public static readonly Permission RelocateStocks = new("stocks:relocate");

    public static readonly Permission GetReceipts = new("receipts:read");
    public static readonly Permission ModifyReceipts = new("receipts:update");
    public static readonly Permission CreateReceipts = new("receipts:create");

    public static readonly Permission GetCustomers = new("customers:read");
    public static readonly Permission ModifyCustomers = new("customers:update");
    public static readonly Permission CreateCustomers = new("customers:create");

    public static readonly Permission ModifyCustomerContacts = new("customers:contacts:update");
    public static readonly Permission CreateCustomerContacts = new("customers:contacts:create");

    public static readonly Permission ModifyCustomerOutlets = new("customers:outlets:update");
    public static readonly Permission CreateCustomerOutlets = new("customers:outlets:create");

    public static readonly Permission ModifyCustomerProducts = new("customers:products:update");

    public static readonly Permission GetSuppliers = new("suppliers:read");
    public static readonly Permission ModifySuppliers = new("suppliers:update");
    public static readonly Permission CreateSuppliers = new("suppliers:create");

    public static readonly Permission ModifySupplierContacts = new("suppliers:contacts:update");
    public static readonly Permission CreateSupplierContacts = new("suppliers:contacts:create");

    public static readonly Permission ModifySupplierProducts = new("suppliers:products:update");

    public static readonly Permission GetPriceTiers = new("price-tiers:read");
    public static readonly Permission ModifyPriceTiers = new("price-tiers:update");

    public static readonly Permission GetPurchaseOrders = new("purchase-orders:read");
    public static readonly Permission ModifyPurchaseOrders = new("purchase-orders:update");
    public static readonly Permission CreatePurchaseOrders = new("purchase-orders:create");

    public static readonly Permission ApprovePurchaseOrders = new("purchase-orders:approve");
    public static readonly Permission CancelPurchaseOrders = new("purchase-orders:cancel");

    public static readonly Permission GetDrivers = new("drivers:read");
    public static readonly Permission ModifyDrivers = new("drivers:update");
    public static readonly Permission CreateDrivers = new("drivers:create");

    public static readonly Permission GetRoutes = new("routes:read");
    public static readonly Permission ModifyRoutes = new("routes:update");
    public static readonly Permission CreateRoutes = new("routes:create");

    public static readonly Permission GetRouteTrips = new("route-trip:read");
    public static readonly Permission ModifyRouteTrips = new("route-trip:update");
    public static readonly Permission CreateRouteTrips = new("route-trip:create");

    public static readonly Permission ModifyRouteTriplogs = new("route-trip-log:update");
    public static readonly Permission ModifyOtherRouteTripLogs = new("route-trip-log:update:other");

    public static readonly Permission GetPutAwayTasks = new("tasks:putaway:read");
    public static readonly Permission ModifyPutAwayTasks = new("tasks:putaway:update");
    public static readonly Permission ManagePutAwayTasks = new("tasks:putaway:manage");
    public static readonly Permission ExecutePutAwayTasks = new("tasks:putaway:execute");

    public static readonly Permission GetWarehouseOperators = new("warehouse-operators:read");
    public static readonly Permission ModifyWarehouseOperators = new("warehouse-operators:update");
    public static readonly Permission CreateWarehouseOperators = new("warehouse-operators:create");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
