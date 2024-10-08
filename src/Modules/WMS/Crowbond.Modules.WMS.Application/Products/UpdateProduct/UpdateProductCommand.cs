﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, ProductRequest Product) : ICommand;

