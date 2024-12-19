using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Locations.DeleteLocation;

public sealed record DeleteLocationCommand(Guid LocationId): ICommand;
