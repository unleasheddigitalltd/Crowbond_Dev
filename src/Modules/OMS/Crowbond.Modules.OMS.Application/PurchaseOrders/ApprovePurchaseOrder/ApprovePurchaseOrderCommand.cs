﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.ApprovePurchaseOrder;

public sealed record ApprovePurchaseOrderCommand(Guid PurchaseOrderHeaderId) : ICommand;
