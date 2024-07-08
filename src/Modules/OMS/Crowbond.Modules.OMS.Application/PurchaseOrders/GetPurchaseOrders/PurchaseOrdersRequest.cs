namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

public sealed record PurchaseOrdersRequest(

    string Search = "",

    string sort = "name",

    string order = "asc",

    int page = 1,

    int size = 10

);
