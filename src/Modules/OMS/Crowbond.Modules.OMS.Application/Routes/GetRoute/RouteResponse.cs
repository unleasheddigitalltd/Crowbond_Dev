namespace Crowbond.Modules.OMS.Application.Routes.GetRoute;

public sealed record RouteResponse(Guid Id, string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek);
