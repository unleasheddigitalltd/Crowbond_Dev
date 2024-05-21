using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Attendance.Application.Attendees.UpdateAttendee;

public sealed record UpdateAttendeeCommand(Guid AttendeeId, string FirstName, string LastName) : ICommand;
