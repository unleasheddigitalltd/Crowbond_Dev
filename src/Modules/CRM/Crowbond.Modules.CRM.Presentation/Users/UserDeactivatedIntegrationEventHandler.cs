﻿using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Users.DeactivateUser;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Users;

internal sealed class UserDeactivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserDeactivatedIntegrationEvent>
{
    public override async Task Handle(
        UserDeactivatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new DeactiveUserCommand(
            integrationEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(DeactiveUserCommand), result.Error);
        }
    }
}
