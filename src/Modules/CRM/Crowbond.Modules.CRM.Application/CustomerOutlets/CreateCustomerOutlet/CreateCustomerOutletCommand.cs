using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.CreateCustomerOutlet;

public sealed record CreateCustomerOutletCommand(Guid CustomerId, CustomerOutletRequest CustomerOutlet) : ICommand<Guid>;
