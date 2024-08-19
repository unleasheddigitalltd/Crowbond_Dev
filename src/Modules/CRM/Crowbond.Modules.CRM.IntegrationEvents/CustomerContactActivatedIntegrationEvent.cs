﻿using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.CRM.IntegrationEvents;

public sealed class CustomerContactActivatedIntegrationEvent: IntegrationEvent
{
    public CustomerContactActivatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}
