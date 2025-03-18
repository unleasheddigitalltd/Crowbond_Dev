using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetOutletRouteForDay;

public sealed record GetOutletRouteForDayQuery(Guid CustomerOutletId, DayOfWeek Day) : IQuery<OutletRouteForDayResponse>;
