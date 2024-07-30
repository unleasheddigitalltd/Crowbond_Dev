using System;
using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Drivers.CreateDriver;

public sealed record CreateDriverCommand(Guid UserId, DriverRequest Driver) : ICommand<Guid>;

