using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;

public sealed record AddDispatchLinesCommand(Guid RouteTripId, List<DispatchLineRequest> DispatchLines) : ICommand;
