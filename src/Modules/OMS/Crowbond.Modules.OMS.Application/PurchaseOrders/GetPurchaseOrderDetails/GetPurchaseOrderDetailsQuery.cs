﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderDetails;

public sealed record GetPurchaseOrderDetailsQuery(Guid PurchaseOrderHeaderId) : IQuery<PurchaseOrderDetailsResponse>;

