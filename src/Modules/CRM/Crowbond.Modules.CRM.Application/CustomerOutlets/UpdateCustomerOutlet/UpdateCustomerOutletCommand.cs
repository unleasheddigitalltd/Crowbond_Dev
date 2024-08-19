using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

public sealed record UpdateCustomerOutletCommand(Guid UserId, Guid CustomerOutletId, CustomerOutletRequest CustomerOutlet) : ICommand;
