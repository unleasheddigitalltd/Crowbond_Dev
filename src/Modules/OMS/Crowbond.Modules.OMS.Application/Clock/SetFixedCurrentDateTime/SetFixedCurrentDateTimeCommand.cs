using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Clock.SetFixedCurrentDateTime;

public sealed record SetFixedCurrentDateTimeCommand(DateTime CurrentDateTime): ICommand;
