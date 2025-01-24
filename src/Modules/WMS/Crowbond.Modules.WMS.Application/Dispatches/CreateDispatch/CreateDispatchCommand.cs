using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;

public sealed record CreateDispatchCommand(Guid RouteTripId, DateOnly RouteTripDate, string RouteName) : ICommand;
