using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Routes.CreateRoute;

public sealed record CreateRouteCommand(string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek) : ICommand<Guid>;
