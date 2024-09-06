using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.DeleteCustomerOutlet;

public sealed record DeleteCustomerOutletCommand(Guid CustomerOutletId) : ICommand;

