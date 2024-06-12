﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;

namespace Crowbond.Modules.WMS.Application.Products.GetProducts;

public sealed record GetProductQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<ProductsResponse>;
