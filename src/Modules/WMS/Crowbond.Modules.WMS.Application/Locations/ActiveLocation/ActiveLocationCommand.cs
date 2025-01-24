using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.ActiveLocation;

public sealed record ActiveLocationCommand(Guid LocationId) : ICommand;
