using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Drivers.AddDriver;

public sealed record AddDriverCommand(Guid UserId) : ICommand;

