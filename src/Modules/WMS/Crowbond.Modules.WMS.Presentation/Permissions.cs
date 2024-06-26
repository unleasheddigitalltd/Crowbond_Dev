﻿namespace Crowbond.Modules.WMS.Presentation;

internal static class Permissions
{
    internal const string GetProducts = "products:read";
    internal const string ModifyProducts = "products:update";
    internal const string CreateProducts = "products:create";

    internal const string GetStocks = "stocks:read";
    internal const string GetStockTransactions = "stocktransactions:read";
    internal const string AdjustStocks = "stocks:adjust";
    internal const string RelocateStocks = "stocks:relocate";

    internal const string GetReceipts = "receipts:read";
    internal const string ModifyReceipts = "receipts:update";
}
