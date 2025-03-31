using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.RetryRouteAssignment;

public sealed record RetryRouteAssignmentCommand(Guid OrderId) : ICommand;
