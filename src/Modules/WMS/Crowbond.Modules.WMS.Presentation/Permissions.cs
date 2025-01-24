namespace Crowbond.Modules.WMS.Presentation;

internal static class Permissions
{
    internal const string GetLocations = "locations:read";
    internal const string ModifyLocations = "locations:update";
    internal const string CreateLocations = "locations:create";
    internal const string DeleteLocations = "locations:delete";

    internal const string GetProducts = "products:read";
    internal const string ModifyProducts = "products:update";
    internal const string CreateProducts = "products:create";
    internal const string CreateProductGroups = "product-groups:create";
    internal const string CreateBrands = "brands:create";
    internal const string CreateCategories = "categories:create";


    internal const string GetStocks = "stocks:read";
    internal const string GetStockTransactions = "stock-transactions:read";
    internal const string CreateStocks = "stocks:create";
    internal const string AdjustStocks = "stocks:adjust";
    internal const string RelocateStocks = "stocks:relocate";


    internal const string GetReceipts = "receipts:read";
    internal const string ModifyReceipts = "receipts:update";
    internal const string CreateReceipts = "receipts:create";

    internal const string GetPutAwayTasks = "tasks:putaway:read";
    internal const string ModifyPutAwayTasks = "tasks:putaway:update";
    internal const string ManagePutAwayTasks = "tasks:putaway:manage";
    internal const string ExecutePutAwayTasks = "tasks:putaway:execute";

    internal const string ExecutePickingTasks = "tasks:picking:execute";

    internal const string GetWarehouseOperators = "warehouse-operators:read";
    internal const string ModifyWarehouseOperators = "warehouse-operators:update";
    internal const string CreateWarehouseOperators = "warehouse-operators:create";
}
