using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductByOrder;

public sealed record CreateCustomerProductByOrderCommand(Guid CustomerId, Guid ProductId): ICommand;
