using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomerActivation;

public sealed record UpdateCustomerActivationCommand(Guid UserId, Guid CustomerId, bool IsActive) : ICommand;
