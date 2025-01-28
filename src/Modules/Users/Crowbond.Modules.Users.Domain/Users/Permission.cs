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
    public static readonly Permission GetTickets = new("tickets:read");
    public static readonly Permission CheckInTicket = new("tickets:check-in");
    public static readonly Permission GetEventStatistics = new("event-statistics:read");

    public static readonly Permission GetLocations = new("locations:read");
    public static readonly Permission ModifyLocations = new("locations:update");
    public static readonly Permission CreateLocations = new("locations:create");
    public static readonly Permission DeleteLocations = new("locations:delete");

    public static readonly Permission GetProducts = new("products:read");
    public static readonly Permission CreateProducts = new("products:create");
    public static readonly Permission ModifyProducts = new("products:update");
    public static readonly Permission CreateProductGroups = new("product-groups:create");
    public static readonly Permission CreateBrands = new("brands:create");
    public static readonly Permission CreateCategories = new("categories:create");

    public static readonly Permission GetStocks = new("stocks:read");
    public static readonly Permission GetStockTransactions = new("stock-transactions:read");
    public static readonly Permission CreateStocks = new("stocks:create");
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

    public static readonly Permission CreateCustomerProducts = new("customers:products:create");
    public static readonly Permission ModifyCustomerProducts = new("customers:products:update");
    public static readonly Permission GetCustomerProducts = new("customers:products:read");
    public static readonly Permission DeletCustomerProducts = new("customers:products:delete");

    public static readonly Permission CreateCustomerProductBlacklist = new("customers:product-blacklist:create");
    public static readonly Permission ModifyCustomerProductBlacklist = new("customers:product-blacklist:update");
    public static readonly Permission GetCustomerProductBlacklist = new("customers:product-blacklist:read");
    public static readonly Permission DeletCustomerProductBlacklist = new("customers:product-blacklist:delete");

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

    public static readonly Permission GetVehicles = new("vehicles:read");
    public static readonly Permission ModifyVehicles = new("vehicles:update");
    public static readonly Permission CreateVehicles = new("vehicles:create");

    public static readonly Permission GetRoutes = new("routes:read");
    public static readonly Permission ModifyRoutes = new("routes:update");
    public static readonly Permission CreateRoutes = new("routes:create");

    public static readonly Permission GetRouteTrips = new("route-trip:read");
    public static readonly Permission ModifyRouteTrips = new("route-trip:update");
    public static readonly Permission CreateRouteTrips = new("route-trip:create");
    public static readonly Permission ApproveRouteTrip = new("route-trip:approve");

    public static readonly Permission ModifyOtherRouteTripLogs = new("route-trip-log:update:other");
    public static readonly Permission GetCompliances = new("compliances:read");

    public static readonly Permission GetPutAwayTasks = new("tasks:putaway:read");
    public static readonly Permission ModifyPutAwayTasks = new("tasks:putaway:update");
    public static readonly Permission ManagePutAwayTasks = new("tasks:putaway:manage");
    public static readonly Permission ExecutePutAwayTasks = new("tasks:putaway:execute");

    public static readonly Permission GetPickingTasks = new("tasks:picking:read");
    public static readonly Permission ModifyPickingTasks = new("tasks:picking:update");
    public static readonly Permission ManagePickingTasks = new("tasks:picking:manage");
    public static readonly Permission ExecutePickingTasks = new("tasks:picking:execute");

    public static readonly Permission GetWarehouseOperators = new("warehouse-operators:read");
    public static readonly Permission ModifyWarehouseOperators = new("warehouse-operators:update");
    public static readonly Permission CreateWarehouseOperators = new("warehouse-operators:create");

    public static readonly Permission GetCart = new("carts:read");
    public static readonly Permission AddToCart = new("carts:add");
    public static readonly Permission RemoveFromCart = new("carts:remove");
    public static readonly Permission GetOrders = new("orders:read");
    public static readonly Permission GetMyOrders = new("orders:read:my");
    public static readonly Permission CreateOrders = new("orders:create");
    public static readonly Permission CreateMyOrders = new("orders:create:my");
    public static readonly Permission ModifyOrders = new("orders:update");
    public static readonly Permission DeleteOrders = new("orders:delete");
    public static readonly Permission AcceptOrders = new("orders:accept");
    public static readonly Permission DeliverOrders = new("orders:deliver");
    public static readonly Permission ReviewOrderLine = new ("order-lines:review");
    public static readonly Permission GetOrderLineRejectReasons = new ("order-line-reject-reasons:read");

    public static readonly Permission GetSettings = new ("settings:read");
    public static readonly Permission ModifySettings = new ("settings:update");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
