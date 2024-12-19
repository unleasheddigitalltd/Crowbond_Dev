using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.HoldLocation;

public sealed record HoldLocationCommand(Guid LocationId) : ICommand;
