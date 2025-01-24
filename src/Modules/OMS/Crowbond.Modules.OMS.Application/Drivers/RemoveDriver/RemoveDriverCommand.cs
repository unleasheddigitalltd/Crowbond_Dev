using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Drivers.RemoveDriver;

public sealed record RemoveDriverCommand(Guid UserId) : ICommand;
