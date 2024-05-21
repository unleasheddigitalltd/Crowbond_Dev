using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;
using Crowbond.Modules.Attendance.Application.Attendees.UpdateAttendee;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Attendance.Presentation.Attendees;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateAttendeeCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateAttendeeCommand), result.Error);
        }
    }
}
