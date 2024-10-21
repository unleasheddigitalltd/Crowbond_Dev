using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Routes.CreateRoute;

public sealed record CreateRouteCommand(Guid RouteId, string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek) : ICommand;
