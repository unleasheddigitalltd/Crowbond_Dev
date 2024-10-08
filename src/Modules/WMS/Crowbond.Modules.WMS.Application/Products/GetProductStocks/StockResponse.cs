﻿namespace Crowbond.Modules.WMS.Application.Products.GetProductStocks;

public sealed record StockResponse(
    string Batch,
    decimal InStock,
    decimal Available,
    decimal Allocated,
    decimal OnHold,
    string Location,
    int DaysInStock);
