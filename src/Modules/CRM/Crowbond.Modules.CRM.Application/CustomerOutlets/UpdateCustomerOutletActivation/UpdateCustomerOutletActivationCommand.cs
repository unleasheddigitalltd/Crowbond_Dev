using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutletActivation;

public sealed record UpdateCustomerOutletActivationCommand(Guid UserId, Guid CustomerOutletId, bool IsActive) : ICommand;
