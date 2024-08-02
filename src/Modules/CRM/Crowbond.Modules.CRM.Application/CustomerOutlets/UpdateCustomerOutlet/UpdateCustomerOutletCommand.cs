using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

public sealed record UpdateCustomerOutletCommand(Guid CustomerOutletId, Guid UserId, CustomerOutletRequest CustomerOutlet) : ICommand;
