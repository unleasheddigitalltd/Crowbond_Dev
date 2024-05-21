using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatisticsResponse>;
