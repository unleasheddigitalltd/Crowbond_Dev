using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.UpdateRoute;

public sealed record UpdateRouteCommand(Guid RouteId, string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek) : ICommand;
