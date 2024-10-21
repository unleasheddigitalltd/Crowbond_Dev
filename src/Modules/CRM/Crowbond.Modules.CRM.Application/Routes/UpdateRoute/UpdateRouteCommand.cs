using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Routes.UpdateRoute;

public sealed record UpdateRouteCommand(Guid RouteId, string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek) : ICommand;
